using System.Text.Json;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Configuration;

public class JsonConfigReader
{
    public ConfigModel ReadFromJsonFile(string path)
    {
        using var fs = new FileStream(path, FileMode.Open);
        var config = JsonSerializer.Deserialize<ConfigModel>(fs);
        if (config is null) throw new GeneticAlgoException("Config file incorrect");
        return config;
    }
}