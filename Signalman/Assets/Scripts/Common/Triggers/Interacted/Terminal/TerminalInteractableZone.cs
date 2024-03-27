using UnityEngine;
using Zenject;

public class TerminalInteractableZone : InteractableZone
{
    [SerializeField] private CameraEnemy _cameraEnemy;
    [SerializeField] private TerminalPromtedTextZone _promted;

    [Inject]
    private Timer _timer;

    protected override async void OnInteract()
    {
        var miniGameMenu = await MiniGameMenu.Load();

        if (_promted != null)
            _promted.OnInteract();

        //_signalBus.Fire(new PauseSignal(true));
        bool isConfirmed = await miniGameMenu.Value.Show();

        if (!miniGameMenu.Value.IsExit)
        {
            if (isConfirmed)
                _cameraEnemy.OffCamera();
            else
            {
                _timer.ReduceTime(10);
                OnEndInteract();
            }
        }
        else
            OnEndInteract();

       // _signalBus.Fire(new PauseSignal(false));
        miniGameMenu.Dispose();
    }
}
