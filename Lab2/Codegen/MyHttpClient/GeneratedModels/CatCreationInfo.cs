namespace MyHttpClient.GeneratedModels;
public class CatCreationInfo
{
    public CatCreationInfo(string name, string breed, JavaDateFormat birthdate)
    {
        Name = name;
        Breed = breed;
        Birthdate = birthdate;
    }

    public string Name { get; set; }

    public string Breed { get; set; }

    public JavaDateFormat Birthdate { get; set; }
}