using Codegen.JavaParser.SemanticDataModels;

namespace Codegen.JavaParser;

public interface IMethodInfoParser
{
    void FillMethodData(string pathToFile, ControllerModel controllerModel);
}