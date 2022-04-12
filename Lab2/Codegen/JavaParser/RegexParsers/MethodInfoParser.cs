using System.Text.RegularExpressions;
using JavaParser.SemanticDataModels;
using JavaParser.Tools;

namespace JavaParser.RegexParsers;

public class MethodInfoParser : IMethodInfoParser
{
    private readonly Regex _methodRegex = new Regex(@"public\s+[A-Za-z0-9<,>]+\s+\w+\s*\([@A-Za-z0-9<, >]*\)");
    private readonly Regex _requestAttributeRegex = new Regex(@"@\w+Mapping");
    private readonly IJavaToCSharpTypeMapper _typeMapper;

    public MethodInfoParser(IJavaToCSharpTypeMapper typeMapper)
    {
        _typeMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));
    }

    public void FillMethodData(string pathToFile, ControllerModel controllerModel)
    {
        var fileStrings = File.ReadAllLines(pathToFile);
        
        var methods = fileStrings.SelectMany(s => _methodRegex.Matches(s)).ToList();
        var methodsClear = methods
            .Select(f => Regex.Replace(f.Value, @"\s+", " ").Replace("(", " ("))
            .ToList();
        var requestInfo = GetRequestInfo(fileStrings);
        var methodCounter = 0;

        foreach (var method in methodsClear)
        {
            var methodModel = new RequestMethodModel();
            var words = method.Split(' ');
            methodModel.Name = words[2];
            methodModel.ReturnType = _typeMapper.MapType(words[1]);
            (methodModel.Url, methodModel.RequestType) = requestInfo[methodCounter];
            var args = GetArgumentInfo(method);
            args.ForEach(a => methodModel.AddArgument(a));
            controllerModel.AddMethodModel(methodModel);
            methodCounter++;
        }
    }

    private List<(string, RequestType)> GetRequestInfo(string[] fileStrings)
    {
        var attributes = fileStrings.Where(s => _requestAttributeRegex.IsMatch(s)).ToList();
        var attributesClear = attributes.Select(a => Regex.Replace(a, @"\s+", " ")).ToList();
        var result = new List<(string, RequestType)>();
        foreach (var attribute in attributesClear)
        {
            var stringType = attribute[(attribute.IndexOf('@') + 1)..attribute.IndexOf("Mapping")];
            if (stringType == "Request") continue;
            var url = attribute[(attribute.IndexOf('\"') + 1)..attribute.LastIndexOf('\"')];
            
            RequestType type = stringType switch
            {
                "Get" => RequestType.Get,
                "Post" => RequestType.Post,
                "Delete" => RequestType.Delete,
                "Patch" => RequestType.Patch,
                "Put" => RequestType.Put,
                _ => throw new ParserException($"type {stringType} is unknown"),
            };
            
            result.Add((url, type));
        }

        return result;
    }

    private List<ArgumentModel> GetArgumentInfo(string methodString)
    {
        var result = new List<ArgumentModel>();
        var argumentsString = methodString[(methodString.IndexOf('(') + 1)..methodString.IndexOf(')')];
        var arguments = argumentsString.Split(',');
        foreach (var argument in arguments)
        {
            if (string.IsNullOrWhiteSpace(argument)) continue;
            var cleanArgument = argument.Trim(' ');
            var argumentModel = new ArgumentModel();
            var words = cleanArgument.Split(' ');
            argumentModel.Name = words[2];
            argumentModel.Type = _typeMapper.MapType(words[1]);
            argumentModel.RequestType = words[0] switch
            {
                "@RequestBody" => RequestArgumentType.Body,
                "@PathVariable" => RequestArgumentType.Path,
                "@RequestParam" => RequestArgumentType.Query,
                _ => throw new ParserException($"argument type {words[0]} is unknown"),
            };
            result.Add(argumentModel);
        }
        return result;
    }
}