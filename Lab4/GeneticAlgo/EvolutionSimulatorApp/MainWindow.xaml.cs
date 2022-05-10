using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Loggers;
using EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

namespace EvolutionSimulatorApp
{
    public partial class MainWindow : Window
    {
        private const int WindowHeight = 1000;
        private const int WindowWidth = 800;
        private readonly IEvolutionAlgorithm _algorithm;
        private readonly IConfiguration _configuration;
        private readonly uint _cellSize;
        private readonly DispatcherTimer _tickTimer = new ();
        private int _updateScreenWaitMs = 10;
        private int _generationNumber;
        private Population _currentPopulation;
        
        public MainWindow()
        {
            var reader = new JsonConfigReader();
            _configuration = reader.ReadFromJsonFile(@"D:\TechLabs\genalgo_cfg.json");
            var logger = new ConsoleLogger();
            var xSize = WindowWidth / _configuration.MapWidth;
            var ySize = WindowHeight / _configuration.MapHeight;
            _cellSize = Math.Min(xSize, ySize);
            
            InitializeComponent();
            
            _tickTimer.Tick += TickTimerTick; 
            _algorithm = new EvolutionAlgorithmGui(_configuration, logger, _cellSize, SimulatorArea, _updateScreenWaitMs);
            _tickTimer.Interval = TimeSpan.FromMilliseconds(_updateScreenWaitMs);
            _currentPopulation = _algorithm.GenerateStarterPopulation();
        }

        private void TickTimerTick(object? sender, EventArgs e)
        {
            Title = $"SimulatorArea - Generation #{_generationNumber}";
            _currentPopulation = _algorithm.RunGeneration(_generationNumber++, _currentPopulation);
        }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            DrawField();
            _tickTimer.IsEnabled = true;
        }

        private void DrawField()
        {
            for (var i = 0; i < _configuration.MapWidth; i++)
            {
                for (var j = 0; j < _configuration.MapHeight; j++)
                {
                    var rect = new Rectangle  
                    {  
                        Width = _cellSize,  
                        Height = _cellSize,  
                        Fill = Brushes.Black,
                        Stroke = Brushes.Gray,
                    };
                    
                    SimulatorArea.Children.Add(rect);  
                    Canvas.SetTop(rect, j * _cellSize);  
                    Canvas.SetLeft(rect, i * _cellSize); 
                }
            }
        }
    }
}