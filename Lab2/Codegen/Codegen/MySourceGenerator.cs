using System.Text;
using Codegen.ClientGenerators;
using Codegen.ModelGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using ControllerParser = Codegen.JavaParser.RegexParsers.ControllerParser;
using JavaCodeParser = Codegen.JavaParser.JavaCodeParser;
using JavaToCSharpTypeMapper = Codegen.JavaParser.Tools.JavaToCSharpTypeMapper;
using MethodInfoParser = Codegen.JavaParser.RegexParsers.MethodInfoParser;
using RequestModelParser = Codegen.JavaParser.RegexParsers.RequestModelParser;

namespace Codegen;

[Generator]
public class MySourceGenerator : ISourceGenerator
{
    private readonly string _pathToRequestModels =
        @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models";

    private readonly string _pathToControllers =
        @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\controllers";

    private readonly string _baseUrl = "http://localhost:8080";
    
    private IRequestModelGenerator _modelGenerator;
    private IClientGenerator _clientGenerator;

    public void Initialize(GeneratorInitializationContext context)
    {
        var parser = new JavaCodeParser(new ControllerParser(new MethodInfoParser(new JavaToCSharpTypeMapper())),
            new RequestModelParser(new JavaToCSharpTypeMapper()));
        
        _modelGenerator = new RequestModelGenerator(parser);
        _clientGenerator = new ClientGenerator(parser, new RequestMethodGenerator());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var mainSyntaxTree = context.Compilation.SyntaxTrees
            .First(x => x.HasCompilationUnitRoot);

        var directory = Path.GetDirectoryName(mainSyntaxTree.FilePath);
        var pathToProject = Path.Combine(directory, context.Compilation.AssemblyName);
        
        var models = _modelGenerator.GenerateModels(
            _pathToRequestModels,
            pathToProject,
            context.Compilation.AssemblyName);

        var controllers = _clientGenerator.GenerateClient(
            _pathToControllers,
            pathToProject,
            context.Compilation.AssemblyName,
            _baseUrl);
        
        foreach (var (syntaxTree, name) in models)
            context.AddSource(name, SourceText.From(syntaxTree.ToString(), Encoding.UTF8));
        
        foreach (var (syntaxTree, name) in controllers)
            context.AddSource(name, SourceText.From(syntaxTree.ToString(), Encoding.UTF8));
    }
}