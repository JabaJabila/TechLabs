namespace MyHttpClient.GeneratedModels;
public class OwnerViewModel
{
    public long OwnerId { get; set; }

    public string Name { get; set; }

    public JavaDateFormat Birthdate { get; set; }

    public List<CatViewModel> Cats { get; set; }
}