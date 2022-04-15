using Codegen.JavaParser.SemanticDataModels;

namespace Codegen.JavaParser;

public interface IControllerParser
{
    ControllerModel GetControllerModel(string pathToFile);
}