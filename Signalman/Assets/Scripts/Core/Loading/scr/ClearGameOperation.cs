using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ClearGameOperation : ILoadingOperation
{
    public string Description => "Clearing game...";

    private readonly IGameModeCleaner _gameCleanUp;
    private readonly string _newxtNameScene;

    public ClearGameOperation(IGameModeCleaner gameCleanUp, string nameScene = Constants.Scenes.MAIN_MENU)
    {
        _gameCleanUp = gameCleanUp;
        _newxtNameScene = nameScene;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);

        await _gameCleanUp.Cleanup();

        foreach (var factory in _gameCleanUp.Factories)
            await factory.Unload();

        onProgress?.Invoke(0.5f);

        await LoadMenuOperation();

        onProgress?.Invoke(0.75f);

        await UnLoadSceneOperation();

        onProgress?.Invoke(1f);
    }

    private async UniTask LoadMenuOperation()
    {
        var loadOperation = SceneManager.LoadSceneAsync(_newxtNameScene, LoadSceneMode.Additive);

        while (loadOperation.isDone == false)
            await UniTask.Yield();
    }

    private async UniTask UnLoadSceneOperation()
    {
        var unloadOperation = SceneManager.UnloadSceneAsync(_gameCleanUp.SceneName);

        while (unloadOperation.isDone == false)
            await UniTask.Yield();
    }
}
