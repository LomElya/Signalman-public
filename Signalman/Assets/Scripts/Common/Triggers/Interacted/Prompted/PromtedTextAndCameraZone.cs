using Cinemachine;
using UnityEngine;

public class PromtedTextAndCameraZone : PromtedTextZone
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    protected override async void OnInteract()
    {
        //await _promted.AwaitForDecision(_description);
        var promtedAlert = await MessagePanel.Load();

        var priority = _virtualCamera.Priority;

        _virtualCamera.Priority = 20;

    _signalBus.Fire(new PauseSignal(true));

        //_animation.Play();
        await promtedAlert.Value.Show(_description);

        _signalBus.Fire(new PauseSignal(false));

        promtedAlert?.Dispose();

        _virtualCamera.Priority = priority;
    }
}
