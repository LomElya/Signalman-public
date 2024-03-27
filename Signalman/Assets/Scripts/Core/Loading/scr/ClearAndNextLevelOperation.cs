using System;
using Cysharp.Threading.Tasks;

public class ClearAndNextLevelOperation : ILoadingOperation
{
    public string Description => "Loading next level...";

    private readonly Gameplay _gameplay;
    private readonly string _newxtNameScene;

    public ClearAndNextLevelOperation(Gameplay gameplay, string nameScene)
    {
        _gameplay = gameplay;
        _newxtNameScene = nameScene;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);

        await UnloadScene();

        onProgress?.Invoke(0.5f);

        await LoadNextLevelOperation();

        onProgress?.Invoke(0.75f);

        onProgress?.Invoke(1f);
    }

    private async UniTask UnloadScene()
    {
        await _gameplay.Cleanup();

        foreach (var factory in _gameplay.Factories)
            await factory.Unload();
    }

    private async UniTask LoadNextLevelOperation() => await _gameplay.SetGameScene(_newxtNameScene);
}
