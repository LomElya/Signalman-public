using System;
using Cysharp.Threading.Tasks;

public class SoundsLoadingOperation : ILoadingOperation
{
    public string Description => "Sounds loading...";

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.5f);

        AudioUtility.LoadAllVolume();

        await UniTask.WaitForSeconds(0.5f);

        onProgress?.Invoke(0.7f);

        await UniTask.WaitForSeconds(0.1f);

        onProgress?.Invoke(1f);
    }
}
