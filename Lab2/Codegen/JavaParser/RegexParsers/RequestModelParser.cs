using System.Text.RegularExpressions;
using JavaParser.SemanticDataModels;
using JavaParser.Tools;

namespace JavaParser.RegexParsers;

public class RequestModelParser : IRequestModelParser
{
    private readonly Regex _classRegex = new Regex(@"class\s+\w+");
    private readonly Regex _fieldRegex = new Regex(@"private\s+\w+<?\w+>?\s+\w+;");
    private readonly IJavaToCSharpTypeMapper _typeMapper;

    public RequestModelParser(IJavaToCSharpTypeMapper typeMapper)
    {
        _typeMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));
    }

    public RequestModel GetModelInfo(string pathToFile)
    {
        var result = new RequestModel();
        
        var fileStrings = File.ReadAllLines(pathToFile);
        var match = fileStrings.FirstOrDefault(s => _classRegex.IsMatch(s));
        if (match is null)
            throw new ParserException("Class name not found");

        match = _classRegex.Match(match).Value;
        result.ModelName = match[(match.LastIndexOf(' ') + 1)..];

        var fields = fileStrings.SelectMany(s => _fieldRegex.Matches(s)).ToList();
        var fieldsClear = fields.Select(f => Regex.Replace(f.Value, @"\s+", " ")).ToList();

        foreach (var field in fieldsClear)
        {
            var words = field[..^1].Split(' ');
            result.AddName(char.ToUpper(words[2][0]) + words[2][1..]);
            result.AddType(_typeMapper.MapType(words[1]));
        }
        
        return result;
    }
}