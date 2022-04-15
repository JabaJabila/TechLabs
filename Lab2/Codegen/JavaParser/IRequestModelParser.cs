using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IRequestModelParser
{
    RequestModel GetModelInfo(string pathToFile);
}