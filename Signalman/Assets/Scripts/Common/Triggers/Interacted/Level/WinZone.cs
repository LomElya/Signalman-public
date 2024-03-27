using UnityEngine;
using Zenject;

public class WinZone : InteractableZoneBase
{
    private Level _level;

    [Inject]
    private void Construct(Level level)
    {
        _level = level;
    }

    protected override void OnInteract() { }

    protected override void Enter(Interacter interacter)
    {
        Interact();
        
        _level.NextLevel();
    }
}
