using UnityEngine;
using Zenject;

public class JumpState : PlayerState
{
    private const string JumpKey = "IsJumping";
    private const string LandelKey = "IsLandel";

    private PlayerStateController _stateController;

    protected float JumpVelocity => _player.JumpVelocity;

    protected Vector3 _velocity;

    public JumpState(Player player) : base(player)
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

        _player.View.SetAnimate(LandelKey, false);
        _player.View.SetAnimate(JumpKey, true);

        if (_player.IsGrounded)
            OnJump();

        _stateController.SetState(PlayerStateType.InAir);
    }

    public override void OnExit() { }

    private void OnJump()
    {
        //_stateController.SetState(PlayerStateType.InAir);
        _player.Rigidbody.velocity = new Vector3(_player.Rigidbody.velocity.x, JumpVelocity * 1.5f, _player.Rigidbody.velocity.z);
    }


}
