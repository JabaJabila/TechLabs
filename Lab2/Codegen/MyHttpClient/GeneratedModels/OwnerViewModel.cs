namespace MyHttpClient.GeneratedModels;
public class OwnerViewModel
{
    public OwnerViewModel(long ownerId, string name, string birthdate, List<CatViewModel> cats)
    {
        OwnerId = ownerId;
        Name = name;
        Birthdate = birthdate;
        Cats = cats;
    }

    public long OwnerId { get; set; }

    public string Name { get; set; }

    public string Birthdate { get; set; }

    public List<CatViewModel> Cats { get; set; }
}