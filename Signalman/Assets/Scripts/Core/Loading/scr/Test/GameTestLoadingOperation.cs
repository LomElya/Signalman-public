using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class GameTestLoadingOperation : ILoadingOperation
{
    public string Description => "Test game loading...";

    private readonly AssetProvider _assetProvider;
    private readonly Level _level;

    public GameTestLoadingOperation(AssetProvider assetProvider, Level level)
    {
        _assetProvider = assetProvider;
        _level = level;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.3f);

        onProgress?.Invoke(0.55f);

        var scene = SceneManager.GetSceneByName(Constants.Scenes.TESTGAMESCEHE);

        var gameplay = scene.GetRoot<Gameplay>();

        //await gameplay.SetGameScene("LevelScene_1");

        onProgress?.Invoke(0.85f);

        await gameplay.Init();

        onProgress?.Invoke(1.0f);
    }
}