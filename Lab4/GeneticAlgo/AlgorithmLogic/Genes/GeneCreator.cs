using AlgorithmLogic.Genes.ActiveGenes;
using AlgorithmLogic.Genes.PassiveGenes;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Genes;

public class GeneCreator : IGeneFactory
{
    private static readonly Random Random;
    private static readonly int[] Codes
        = { 0,10,11,12,13,14,15,16,17,20,21,22,23,24,25,26,27,30,31,32,33,34,35,36,37,40,41,42,43,44,45,46,47 };
    
    static GeneCreator()
    {
        Random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    }
    
    public IGene CreateRandomGene()
    {
        return CreateGeneFromCode(GetRandomCode());
    }

    public IGene CreateGeneFromCode(int code)
    {
        return (code / 10) switch
        {
            0 => new ConditionalTransitionGene(),
            1 => new EatingGene(code % 10),
            2 => new EatingGene(code % 10),
            3 => new NeutralizingGene(code % 10),
            4 => new UnconditionalTransitionGene(code % 10),
            _ => throw new GeneticAlgoException($"Gene with code {code} doesn't exist"),
        };
    }

    private static int GetRandomCode()
    {
        return  Codes[Random.Next(0, Codes.Length)];
    }
}