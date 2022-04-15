using Codegen.JavaParser.SemanticDataModels;

namespace Codegen.JavaParser;

public interface IRequestModelParser
{
    RequestModel GetModelInfo(string pathToFile);
}