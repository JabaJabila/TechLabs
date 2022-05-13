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
    public partial class MainWindow
    {
        private readonly IEvolutionAlgorithm _algorithm;
        private const int UpdateScreenAfterGenerationWaitMs = 100;
        private const int UpdateScreenAfterIterationWaitMs = 10;
        private int _generationNumber;
        private Population _currentPopulation;
        
        public MainWindow()
        {
            var reader = new JsonConfigReader();
            IConfiguration configuration = reader.ReadFromJsonFile(@"D:\TechLabs\Lab4\genalgo_cfg.json");
            var logger = new ConsoleLogger();

            InitializeComponent();
            
            _algorithm = new EvolutionAlgorithmGui(configuration, logger, ImageView, UpdateScreenAfterIterationWaitMs);
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
                Application.Current.Dispatcher.Invoke(() => Title = $"SimulatorArea - Generation #{_generationNumber}");
                _currentPopulation = _algorithm.RunGeneration(_generationNumber++, _currentPopulation);
                Thread.Sleep(UpdateScreenAfterGenerationWaitMs);
            }
        }
    }
}