namespace AlgorithmLogic.Genes;

public interface IGeneFactory
{
    IGene CreateRandomGene();
    IGene CreateGeneFromCode(int code);
}