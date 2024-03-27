using UnityEngine;
using Zenject;

public abstract class InteractableZone : InteractableZoneBase
{
    [SerializeField] protected InteractedView _view;

    [Inject] protected SignalBus _signalBus;

    public override void Interact()
    {
        base.Interact();

        _view.Hide();
    }

    protected override void Enter(Interacter interacter)
    {
        _view.Show();
        interacter.Enter(this);
    }

    protected override void Exit(Interacter interacter)
    {
        interacter.Exit();
        _view.Hide();
    }
}
