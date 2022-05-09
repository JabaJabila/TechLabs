using System.Text.Json;
using AlgorithmLogic.Configuration;

// using var fs = new FileStream(@"D:\TechLabs\genalgo_cfg.json", FileMode.Create);
// var options = new JsonSerializerOptions { WriteIndented = true };
// var cfg = new ConfigModel(1000, 700, 30, 10, 3, 1, 40, 22, 100, 10, 100, 3, 5, 10);
// JsonSerializer.Serialize(fs, cfg, options);

var reader = new JsonConfigReader();
var cfg = reader.ReadFromJsonFile(@"D:\TechLabs\genalgo_cfg.json");