/// <summary>
/// Interface for MonoBehaviour instances, that can be created from it's models
/// </summary>
public interface IActor
{
    public void Initialize(IInstanceModel model);
}