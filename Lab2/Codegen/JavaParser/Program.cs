using JavaParser;
using JavaParser.RegexParsers;
using JavaParser.Tools;

var typeMapper = new JavaToCSharpTypeMapper();
var parser = new JavaCodeParser(
    new ControllerParser(new MethodInfoParser(typeMapper)),
    new RequestModelParser(typeMapper));

var models = parser
    .ParseAllRequestModels(@"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models");

foreach (var model in models)
{
    Console.WriteLine(model.ModelName);
    var props = model.PropertyTypes.Zip(model.PropertyNames, (first, second) => first + " " + second);
    foreach (var prop in props)
        Console.WriteLine(prop);
    
    Console.WriteLine();
}
Console.WriteLine();

var controllers = parser
    .ParseAllControllers(@"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\controllers");

foreach (var controller in controllers)
{
    Console.WriteLine(controller.Name);
    Console.WriteLine(controller.BaseUrl);
    foreach (var method in controller.MethodModels)
    {
        Console.WriteLine($"{method.Url} {method.RequestType.ToString()}");
        Console.WriteLine($"{method.ReturnType} {method.Name}");
        foreach (var arg in method.Arguments)
        {
            Console.WriteLine($"\t{arg.RequestType} {arg.Type} {arg.Name}");
        }
        Console.WriteLine("------------------------------------------");
    }
    
    Console.WriteLine();
}