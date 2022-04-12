using JavaParser.RegexParsers;
using JavaParser.Tools;

var typeMapper = new JavaToCSharpTypeMapper();

var modelParser = new RequestModelParser(typeMapper);
var model = modelParser
    .GetModelInfo(@"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models\CatViewModel.java");
    
Console.WriteLine(model.ModelName);
var props = model.PropertyTypes.Zip(model.PropertyNames, (first, second) => first + " " + second);
foreach (var prop in props)
{
    Console.WriteLine(prop);
}

Console.WriteLine();
var controllerParser = new ControllerParser(new MethodInfoParser(typeMapper));
var controller = controllerParser
    .GetControllerModel(@"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\controllers\CatController.java");
    
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