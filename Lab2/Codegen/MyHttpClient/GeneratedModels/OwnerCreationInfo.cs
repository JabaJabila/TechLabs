namespace MyHttpClient.GeneratedModels;
public class OwnerCreationInfo
{
    public OwnerCreationInfo(string name, JavaDateFormat birthdate)
    {
        Name = name;
        Birthdate = birthdate;
    }

    public string Name { get; set; }

    public JavaDateFormat Birthdate { get; set; }
}