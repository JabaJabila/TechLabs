using Codegen.JavaParser.Tools;

namespace Codegen.JavaParser.SemanticDataModels;

public class RequestMethodModel
{
    private readonly List<ArgumentModel> _args;

    public RequestMethodModel()
    {
        _args = new List<ArgumentModel>();
    }
    
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public RequestType RequestType { get; set; }
    public string Url { get; set; }
    public IReadOnlyCollection<ArgumentModel> Arguments => _args;

    public void AddArgument(ArgumentModel argumentModel) => _args.Add(argumentModel);
}