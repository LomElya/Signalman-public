using Zenject;

public class LandedState : PlayerState
{
    private const string LandelKey = "IsLandel";

    private PlayerStateController _stateController;

    public LandedState(Player player) : base(player)
    {
    }

    [Inject]
    private void Construct(PlayerStateController stateController)
    {
        _stateController = stateController;
    }

    public override void OnEnter()
    {
        if (_player == null)
            return;

        _player.View.SetAnimate(LandelKey, true);
        _stateController.SetState(PlayerStateType.Idle);
    }

    public override void OnExit()
    {
        // _player.View.SetAnimate(LandelKey, false);
    }
}
