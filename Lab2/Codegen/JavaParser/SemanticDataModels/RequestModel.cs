namespace JavaParser.SemanticDataModels;

public class RequestModel
{
    private readonly List<string> _names;
    private readonly List<string> _types;

    public RequestModel()
    {
        _names = new List<string>();
        _types = new List<string>();
        DtoName = string.Empty;
    }
    
    public string DtoName { get; set; }
    public IReadOnlyCollection<string> PropertyNames => _names;
    public IReadOnlyCollection<string> PropertyTypes => _types;

    public void AddName(string name) => _names.Add(name);
    public void AddType(string type) => _types.Add(type);
}