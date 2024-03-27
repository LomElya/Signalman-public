using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class GameLoadingOperation : ILoadingOperation
{
    public string Description => "Game loading...";

    private readonly AssetProvider _assetProvider;
    private readonly Level _level;

    public GameLoadingOperation(AssetProvider assetProvider, Level level)
    {
        _assetProvider = assetProvider;
        _level = level;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.3f);

        var loadingOperation = SceneManager.LoadSceneAsync(Constants.Scenes.GAMESCENE, LoadSceneMode.Single);

        while (loadingOperation.isDone == false)
            await UniTask.Yield();

        onProgress?.Invoke(0.55f);

        var scene = SceneManager.GetSceneByName(Constants.Scenes.GAMESCENE);

        var gameplay = scene.GetRoot<Gameplay>();

        await gameplay.SetGameScene("LevelScene_1");

        onProgress?.Invoke(0.85f);

        await gameplay.Init();

        onProgress?.Invoke(1.0f);
    }
}
