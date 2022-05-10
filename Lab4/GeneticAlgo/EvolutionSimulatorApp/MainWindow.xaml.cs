using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
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
        private const int UpdateScreenWaitMs = 1000;
        private int _generationNumber;
        private Population _currentPopulation;
        
        public MainWindow()
        {
            var reader = new JsonConfigReader();
            IConfiguration configuration = reader.ReadFromJsonFile(@"D:\TechLabs\genalgo_cfg.json");
            var logger = new ConsoleLogger();
            // var xSize = WindowWidth / configuration.MapWidth;
            // var ySize = WindowHeight / configuration.MapHeight;
            // var cellSize = Math.Min(xSize, ySize);
            
            InitializeComponent();
            
            _algorithm = new EvolutionAlgorithmGui(configuration, logger, ImageView, UpdateScreenWaitMs);
            _currentPopulation = _algorithm.GenerateStarterPopulation();
            _generationNumber = 1;
            
            var worker = new BackgroundWorker();
            worker.DoWork += StartAlgo;
            worker.RunWorkerAsync();
        }

        private void StartAlgo(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                _currentPopulation = _algorithm.RunGeneration(_generationNumber++, _currentPopulation);
                Thread.Sleep(UpdateScreenWaitMs);
            }
        }
    }
}