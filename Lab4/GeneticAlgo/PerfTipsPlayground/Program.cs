// using AlgorithmLogic.Configuration;
// using AlgorithmLogic.Evolution;
// using AlgorithmLogic.Tools.Loggers;
//
//
// var reader = new JsonConfigReader();
// var cfg = reader.ReadFromJsonFile(@"D:\TechLabs\genalgo_cfg.json");
// var test = new EvolutionNoGui(cfg, new ConsoleLogger());
//
// var population = test.GenerateStarterPopulation();
// var gen = 1;
// while(gen <= 100)
//     population = test.RunGeneration(gen++, population);

using BenchmarkDotNet.Running;
using PerfTipsPlayground;

var summary = BenchmarkRunner.Run<Benchmarking>();