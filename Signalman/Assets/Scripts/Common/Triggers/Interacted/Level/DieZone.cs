using UnityEngine;

public class DieZone : InteractableZoneBase
{
    protected override void OnInteract() { }

    protected override void Enter(Interacter interacter)
    {
        _signalBus.Fire(new GameLoseSignal());
        Debug.Log("Упал");
    }
}
