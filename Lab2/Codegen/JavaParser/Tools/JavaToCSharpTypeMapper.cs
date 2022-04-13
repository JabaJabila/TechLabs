namespace JavaParser.Tools;

public class JavaToCSharpTypeMapper : IJavaToCSharpTypeMapper
{
    public string MapType(string type)
    {
        if (!type.Contains('<')) return MapBaseType(type);
        
        var subType = MapType(type[(type.IndexOf('<') + 1)..type.LastIndexOf('>')]);
        var mainType = MapBaseType(type[..type.IndexOf('<')]);
        return string.IsNullOrWhiteSpace(mainType) ? subType : $"{mainType}<{subType}>";
    }

    private string MapBaseType(string type)
    {
        return type switch
        {
            "ArrayList" => "List",
            "Date" => "string",
            "String" => "string",
            "Integer" => "int",
            "Long" => "long",
            "HttpStatus" => "void",
            "ResponseEntity" => "",
            _ => type,
        };
    }
}