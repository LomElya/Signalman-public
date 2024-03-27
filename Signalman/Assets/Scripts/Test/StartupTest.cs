using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class StartupTest : MonoBehaviour
{
    private Level _level;

    private AssetProvider _assetProvider;
    private LoadingScreenProvider _loadingProvider;

    [Inject]
    private void Construct(AssetProvider assetProvider, LoadingScreenProvider loadingProvider, Level level)
    {
        _assetProvider = assetProvider;
        _loadingProvider = loadingProvider;

        _level = level;
    }

    private void Awake()
    {
        OnLoadGameScene();
    }

    private async void OnLoadGameScene()
    {
        var loadingOperations = new Queue<ILoadingOperation>();

        loadingOperations.Enqueue(_assetProvider);
        loadingOperations.Enqueue(new GameTestLoadingOperation(_assetProvider, _level));

        await _loadingProvider.LoadAndDestroy(loadingOperations);
    }
}
