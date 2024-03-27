using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class SetPausGameCommand : Command
{
    private PauseHandler _pauseHandler;
    private Timer _timer;

    [Inject]
    private void Construct(PauseHandler pauseHandler, Timer timer)
    {
        _pauseHandler = pauseHandler;
        _timer = timer;
    }
    public SetPausGameCommand(SetPausGameData data) : base(data)
    {
    }

    public override async UniTask Execute(Action onCompleted)
    {
        SetPausGameData pauseData = (SetPausGameData)_commandData;
        bool isPaused = pauseData.IsPaused;

        _pauseHandler.SetPause(isPaused);
        _timer.SetPause(isPaused);
        onCompleted?.Invoke();
    }
}
