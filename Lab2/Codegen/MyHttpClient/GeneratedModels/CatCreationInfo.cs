namespace MyHttpClient.GeneratedModels;
public class CatCreationInfo
{
    public CatCreationInfo(string name, string breed, string birthdate)
    {
        Name = name;
        Breed = breed;
        Birthdate = birthdate;
    }

    public string Name { get; set; }

    public string Breed { get; set; }

    public string Birthdate { get; set; }
}