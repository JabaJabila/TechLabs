using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IMethodInfoParser
{
    void FillMethodData(string pathToFile, ControllerModel controllerModel);
}