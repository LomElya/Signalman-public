using System;
using Cysharp.Threading.Tasks;
using Zenject;


public class UnFollowCameraComand : Command
{
    private GameProcessor _processor;

    public UnFollowCameraComand(UnFollowCameraData data) : base(data)
    {
    }

    [Inject]
    private void Construct(GameProcessor processor)
    {
        _processor = processor;
    }

    public override async UniTask Execute(Action onCompleted)
    {
        _processor.UnFollowCamera();

        await UniTask.WaitForSeconds(0.5f);

        onCompleted?.Invoke();
    }
}
