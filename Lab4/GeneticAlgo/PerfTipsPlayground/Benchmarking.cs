using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Loggers;

namespace PerfTipsPlayground;

using BenchmarkDotNet.Attributes;


[MemoryDiagnoser]
public class Benchmarking
{
    private EvolutionNoGui _algorithm;
    
    public Population Population { get; set; }

    [Params(10)]
    public int Generations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var reader = new JsonConfigReader();
        var cfg = reader.ReadFromJsonFile(@"D:\TechLabs\genalgo_cfg.json");
        _algorithm = new EvolutionNoGui(cfg, new ConsoleLogger());
    }

    [IterationSetup]
    public void PopulationGeneration()
    {
        Population = _algorithm.GenerateStarterPopulation();
    }
        
        
    [WarmupCount(3)]
    [Benchmark]
    public void RunGenerations()
    {
        var gen = 1;
        while(gen <= Generations)
            Population = _algorithm.RunGeneration(gen++, Population);
    }
}