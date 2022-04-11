using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IRequestRouteParser
{
    RequestMethodModel GetRequestRoute(string pathToFile, RequestMethodModel modelToFill);
}