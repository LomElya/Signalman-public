using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class LoadSceneCommand : Command
{
    private LoadingScreenProvider _loadingProvider;
    private AssetProvider _assetProvider;
    private Timer _timer;

    private Gameplay _gameplay;

    public LoadSceneCommand(LoadSceneData data) : base(data)
    {
    }

    [Inject]
    private void Construct(LoadingScreenProvider loadingProvider, Timer timer, Gameplay gameplay, AssetProvider assetProvider)
    {
        _loadingProvider = loadingProvider;
        _assetProvider = assetProvider;
        _timer = timer;
        _gameplay = gameplay;
    }

    public override async UniTask Execute(Action onCompleted)
    {
        var data = (LoadSceneData)_commandData;
        string nameScene = data.NameScene;

        await _loadingProvider.LoadAndDestroy(new ClearAndNextLevelOperation(_gameplay, nameScene));

        onCompleted?.Invoke();
    }
}