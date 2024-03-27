using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private AssetProvider _assetProvider;
    private LoadingScreenProvider _loadingProvider;

    [Inject]
    private void Construct(AssetProvider assetProvider, LoadingScreenProvider loadingProvider)
    {
        _assetProvider = assetProvider;
        _loadingProvider = loadingProvider;
    }

    private void Awake()
    {
        OnLoadGameScene();
    }

    private async void OnLoadGameScene()
    {
        var loadingOperations = new Queue<ILoadingOperation>();

        loadingOperations.Enqueue(_assetProvider);
        loadingOperations.Enqueue(new SoundsLoadingOperation());

        await _loadingProvider.LoadAndDestroy(loadingOperations);
    }
}
