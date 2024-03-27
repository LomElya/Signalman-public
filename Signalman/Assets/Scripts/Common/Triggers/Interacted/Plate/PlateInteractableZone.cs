using UnityEngine;

public class PlateInteractableZone : InteractableZone
{
    [SerializeField] private FollowPath _followPath;

    protected override async void OnInteract()
    {
        await _followPath.StartMove();

        OnEndInteract();
    }
}