using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution;
using AlgorithmLogic.Tools.Loggers;

var reader = new JsonConfigReader();
var cfg = reader.ReadFromJsonFile(@"D:\TechLabs\Lab4\genalgo_cfg.json");
var test = new EvolutionNoGui(cfg, new ConsoleLogger());

var population = test.GenerateStarterPopulation();
var gen = 1;
while(true)
    population = test.RunGeneration(gen++, population);