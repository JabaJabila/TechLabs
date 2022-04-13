namespace MyHttpClient.GeneratedModels;
public class OwnerCreationInfo
{
    public OwnerCreationInfo(string name, string birthdate)
    {
        Name = name;
        Birthdate = birthdate;
    }

    public string Name { get; set; }

    public string Birthdate { get; set; }
}