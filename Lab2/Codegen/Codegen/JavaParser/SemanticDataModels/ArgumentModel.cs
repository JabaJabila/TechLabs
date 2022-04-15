using Codegen.JavaParser.Tools;

namespace Codegen.JavaParser.SemanticDataModels;

public class ArgumentModel
{
    public string Name { get; set; }
    public RequestArgumentType RequestType { get; set; }
    public string Type { get; set; }
}