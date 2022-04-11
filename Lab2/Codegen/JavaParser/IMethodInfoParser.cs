using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IMethodInfoParser
{
    RequestMethodModel GetMethodData(string pathToFile);
}