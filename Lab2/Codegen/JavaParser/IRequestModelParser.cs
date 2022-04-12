using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IRequestModelParser
{
    RequestModel GetDtoInfo(string pathToFile);
}