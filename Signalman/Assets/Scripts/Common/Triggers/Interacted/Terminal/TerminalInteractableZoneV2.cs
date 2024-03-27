using System;
using Cinemachine;
using UnityEngine;
using Zenject;

public class TerminalInteractableZoneV2 : InteractableZone
{
    public event Action Interacted;
    public event Action OnExit;

    [SerializeField] private CameraEnemy _cameraEnemy;
    [SerializeField] private TerminalPromtedTextZone _promted;

    [Header("MiniGame")]
    [SerializeField] private GameObject _lightScreen;
    [SerializeField] private MiniGameMenuV2 _miniGame;
    [SerializeField] private CinemachineVirtualCamera _cameraTerminal;

    [Inject]
    private Timer _timer;

    protected async override void OnInteract()
    {
        if (_promted != null)
            _promted.OnInteract();

        var priority = _cameraTerminal.Priority;

        OnEnter();

        bool isConfirmed = await _miniGame.Show();

        if (!_miniGame.IsExit)
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

        EndInteract();
        _cameraTerminal.Priority = priority;
    }

    private void OnEnter()
    {
        _cameraTerminal.Priority = 20;

        _lightScreen.gameObject.SetActive(false);
        Interacted?.Invoke();
    }

    private void EndInteract()
    {
        _lightScreen.gameObject.SetActive(true);
        OnExit?.Invoke();
    }
}
