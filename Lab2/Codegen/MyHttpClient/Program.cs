using Codegen.ModelGenerators;
using JavaParser;
using JavaParser.RegexParsers;
using JavaParser.Tools;

var generator = new RequestModelGenerator(new JavaCodeParser(
    new ControllerParser(new MethodInfoParser(new JavaToCSharpTypeMapper())), new RequestModelParser(new JavaToCSharpTypeMapper())));
generator.GenerateModels(@"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models", @"D:\TechLabs\Lab2\Codegen\MyHttpClient", "MyHttpClient");

