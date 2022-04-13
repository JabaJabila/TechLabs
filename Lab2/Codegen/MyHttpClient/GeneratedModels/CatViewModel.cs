namespace MyHttpClient.GeneratedModels;
public class CatViewModel
{
    public CatViewModel(long catId, string name, string breed, JavaDateFormat birthdate, long ownerId)
    {
        CatId = catId;
        Name = name;
        Breed = breed;
        Birthdate = birthdate;
        OwnerId = ownerId;
    }

    public long CatId { get; set; }

    public string Name { get; set; }

    public string Breed { get; set; }

    public JavaDateFormat Birthdate { get; set; }

    public long OwnerId { get; set; }
}