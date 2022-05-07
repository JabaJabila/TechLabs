using AlgorithmLogic.Genes;

namespace AlgorithmLogic.Evolution.EvolutionEntities;

public class Chromosome
{
    private readonly IGene[] _genotype;
    private int _currentGenePosition;

    public Chromosome(uint geneCount, IGeneFactory geneFactory)
    {
        ArgumentNullException.ThrowIfNull(geneFactory, nameof(geneFactory));
        _genotype = new IGene[geneCount];
        RandomizeGenotype(geneFactory);
    }
    
    public Chromosome(IGene[] genotype)
    {
        _genotype = genotype ?? throw new ArgumentNullException(nameof(genotype));
    }
    
    public int CurrentGenePosition
    {
        get => _currentGenePosition;
        set => _currentGenePosition = value % _genotype.Length; 
    }
    
    public IReadOnlyCollection<IGene> Genotype => _genotype;
    
    private void RandomizeGenotype(IGeneFactory geneFactory)
    {
        for (var i = 0; i < _genotype.Length; i++)
            _genotype[i] = geneFactory.CreateRandomGene();
    }
}