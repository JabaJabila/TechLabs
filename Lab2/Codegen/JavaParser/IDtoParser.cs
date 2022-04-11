using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IDtoParser
{
    DtoModel GetDtoInfo(string pathToFile);
}