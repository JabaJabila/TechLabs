using JavaParser.RegexParsers;
using JavaParser.SemanticDataModels;
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