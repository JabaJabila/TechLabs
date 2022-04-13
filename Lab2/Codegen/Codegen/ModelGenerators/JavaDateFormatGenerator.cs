using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace Codegen.ModelGenerators;

public class JavaDateFormatGenerator
{
    public void Generate(string pathToProject, string rootNamespace)
    {
        File.WriteAllText(Path.Combine(pathToProject, "JavaDateFormat.cs"), SourceText.From(
            $@"namespace {rootNamespace};

public class JavaDateFormat
{{
    private readonly DateTime _dateTime;
    
    public JavaDateFormat(string date)
    {{
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentNullException(nameof(date));

        var parsedDate = date.Split('-');
        if (parsedDate.Length != 3) throw new ArgumentException(""Wrong date format. Correct format: yyyy-MM-dd"");
        _dateTime = new DateTime(
            Convert.ToInt32(parsedDate[0]),
            Convert.ToInt32(parsedDate[1]),
            Convert.ToInt32(parsedDate[2]));

        Date = date;
    }}

    public string Date {{ get; set; }}

    public DateTime GetDate() => _dateTime;

    public override string ToString()
    {{
        return Date;
    }}
}}", Encoding.UTF8).ToString());
    }
}