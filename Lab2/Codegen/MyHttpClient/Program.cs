using Codegen.ClientGenerators;
using Codegen.ModelGenerators;
using JavaParser;
using JavaParser.RegexParsers;
using JavaParser.Tools;
using MyHttpClient;
using MyHttpClient.GeneratedClient;
using MyHttpClient.GeneratedModels;

var modelGenerator = new RequestModelGenerator(
    new JavaCodeParser(
        new ControllerParser(
            new MethodInfoParser(new JavaToCSharpTypeMapper())),
        new RequestModelParser(new JavaToCSharpTypeMapper())));

var clientGenerator = new ClientGenerator(
    new JavaCodeParser(
        new ControllerParser(
            new MethodInfoParser(new JavaToCSharpTypeMapper())),
        new RequestModelParser(new JavaToCSharpTypeMapper())), new RequestMethodGenerator());

modelGenerator.GenerateModels(
    @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models",
    @"D:\TechLabs\Lab2\Codegen\MyHttpClient", 
    "MyHttpClient");

clientGenerator.GenerateClient(
    @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\controllers",
    @"D:\TechLabs\Lab2\Codegen\MyHttpClient", 
    "MyHttpClient",
    "http://localhost:8080");


// var test = new TestClient("http://localhost:8080");
// var res= await test.CreateOwner(new OwnerCreationInfo("Artemka", "2001-11-29"));
// Console.WriteLine(res.Name);
// var all = await test.GetAll();
// Console.WriteLine(all.First().OwnerId);
// Console.WriteLine(all.First().Cats.Count);
// var first = await test.GetOwner(2);
// Console.WriteLine(first.Birthdate);
// await test.AddCats(1, new List<long> {1, 2, 3});
// await test.DeleteOwner(1);
// all = await test.GetAll();
// Console.WriteLine(all.Count);
