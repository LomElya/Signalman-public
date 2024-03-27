using UnityEngine;
using Zenject;

public abstract class PromtedZone : MonoBehaviour
{
    protected const string KeyTooltips = "Tooltips";

    [SerializeField] protected InteractedZoneTrigger _trigger;

    [Inject] protected SignalBus _signalBus;

    protected bool _canInteract = true;

    protected abstract void OnInteract();

    public void Interact()
    {
        bool isConfirmed =  PlayerExtensions.Load(KeyTooltips);

        if (isConfirmed)
            return;

        _canInteract = false;

        OnInteract();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
        _trigger.Stay += OnStay;
        _trigger.Exit += OnExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
        _trigger.Stay -= OnStay;
        _trigger.Exit -= OnExit;
    }

    private void OnEnter(Interacter interacter)
    {
        if (!_canInteract)
            return;

        Interact();
    }

    private void OnStay(Interacter interacter) => OnEnter(interacter);

    private void OnExit(Interacter interacter) => interacter.Exit();

    protected virtual void OnEndInteract() => _canInteract = true;
}
