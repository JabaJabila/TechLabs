using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IControllerParser
{
    ControllerModel GetControllerModel(string pathToFile);
}