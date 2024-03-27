public class LoadSceneData : CommandData
{
    public string NameScene { get; private set; }

    public LoadSceneData(string nameScene) => NameScene = nameScene;
}
