using UnityEngine;
using Zenject;

public class IdleState : PlayerState
{
    private const string MoveKey = "Speed";
    private const string LandelKey = "IsLandel";

    private IInput _input;
    private PlayerStateController _stateController;

    public IdleState(Player player) : base(player)
    {
    }

    [Inject]
    private void Construct(IInput input, PlayerStateController stateController)
    {
        _input = input;
        _stateController = stateController;
    }

    public override void OnEnter()
    {
        Register();

        if (_player == null)
            return;

        _player.View.SetAnimate(LandelKey, true);
        _player.View.SetAnimate(MoveKey, 0f);
    }

    public override void OnExit()
    {
        Unregister();
    }

    private void Register()
    {
        _input.ClickButtonMove += OnClickMove;
        _input.ClickButtonJump += OnClickJump;
    }

    private void Unregister()
    {
        _input.ClickButtonMove -= OnClickMove;
        _input.ClickButtonJump -= OnClickJump;
    }

    private void OnClickMove(Vector3 direction)
    {
        _stateController.SetState(PlayerStateType.Run);
    }

    private void OnClickJump()
    {
        if (_player == null)
            return;

        _player.View.SetAnimate(LandelKey, false);
        _stateController.SetState(PlayerStateType.Jump);
    }

    public override void Dispose() => OnExit();
}
