namespace JavaParser.SemanticDataModels;

public class ControllerModel
{
    private readonly List<RequestMethodModel> _methods;

    public ControllerModel()
    {
        _methods = new List<RequestMethodModel>();
    }
    
    public string Name { get; set; }
    public string BaseUrl { get; set; }
    public IReadOnlyCollection<RequestMethodModel> MethodModels => _methods;

    public void AddMethodModel(RequestMethodModel methodModel) => _methods.Add(methodModel);
}