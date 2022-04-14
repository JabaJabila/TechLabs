using Codegen.ClientGenerators;
using Codegen.ModelGenerators;
using JavaParser;
using JavaParser.RegexParsers;
using JavaParser.Tools;
using MyHttpClient.GeneratedClient;
using MyHttpClient.GeneratedModels;

var parser = new JavaCodeParser(
    new ControllerParser(
        new MethodInfoParser(new JavaToCSharpTypeMapper())),
    new RequestModelParser(new JavaToCSharpTypeMapper()));

var modelGenerator = new RequestModelGenerator(parser);
var clientGenerator = new ClientGenerator(parser, new RequestMethodGenerator());

modelGenerator.GenerateModels(
    @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\models",
    @"D:\TechLabs\Lab2\Codegen\MyHttpClient", 
    "MyHttpClient");

clientGenerator.GenerateClient(
    @"D:\TechLabs\Lab2\javaServer\src\main\java\com\JabaJabila\javaServer\controllers",
    @"D:\TechLabs\Lab2\Codegen\MyHttpClient", 
    "MyHttpClient",
    "http://localhost:8080");


// var testOwner = new OwnerController();
// var testCat = new CatController();
//
// await testCat.CreateCat(new CatCreationInfo("a", "b", "2020-01-10"));
// await testCat.CreateCat(new CatCreationInfo("b", "c", "2020-01-10"));
//
// var cats = await testCat.GetAll();
// Console.WriteLine(cats.First().Name);
//
// var res= await testOwner.CreateOwner(new OwnerCreationInfo("Artemka", "2001-11-29"));
// Console.WriteLine(res.Name);
//
// var all = await testOwner.GetAll();
// Console.WriteLine(all.First().OwnerId);
// Console.WriteLine(all.First().Cats.Count);
//
// var first = await testOwner.GetOwner(1);
// Console.WriteLine(first.Birthdate);
//
// await testOwner.AddCats(1, new List<long?> {1, 2});
// first = await testOwner.GetOwner(1);
// var firstCat = await testCat.GetCat(1);
// Console.WriteLine(firstCat.OwnerId);
// Console.WriteLine(first.Cats.Count);
//
// await testOwner.DeleteOwner(1);
// all = await testOwner.GetAll();
// Console.WriteLine(all.Count);
//
// var allCat = await testCat.GetAll();
// Console.WriteLine(allCat.Count);