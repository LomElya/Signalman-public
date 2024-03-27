using UnityEngine;
using Zenject;

public abstract class InteractableZoneBase : MonoBehaviour
{
    [SerializeField] protected InteractedZoneTrigger _trigger;

    protected SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    protected bool _canInteract = true;

    private void OnEnable() => Subsctibe();

    private void OnDisable() => Unsubscribe();

    public virtual void Interact()
    {
        if (!_canInteract)
            return;

        _canInteract = false;

        OnInteract();
    }

    protected abstract void OnInteract();

    private void OnEnter(Interacter interacter)
    {
        if (!_canInteract)
            return;

        Enter(interacter);
    }

    private void OnStay(Interacter interacter)
    {
        if (!_canInteract)
            return;

        OnEnter(interacter);

        Stay(interacter);
    }

    private void OnExit(Interacter interacter)
    {
        interacter.Exit();

        Exit(interacter);
    }

    protected virtual void OnEndInteract()
    {
        _canInteract = true;
        Subsctibe();
    }

    protected virtual void Subsctibe()
    {
        _trigger.Enter += OnEnter;
        _trigger.Stay += OnStay;
        _trigger.Exit += OnExit;
    }

    protected virtual void Unsubscribe()
    {
        _trigger.Enter -= OnEnter;
        _trigger.Stay -= OnStay;
        _trigger.Exit -= OnExit;
    }

    protected virtual void Enter(Interacter interacter) { }
    protected virtual void Stay(Interacter interacter) { }
    protected virtual void Exit(Interacter interacter) { }
}
