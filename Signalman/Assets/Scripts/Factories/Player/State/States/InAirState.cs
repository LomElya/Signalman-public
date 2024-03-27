using UnityEngine;
using Zenject;

public class InAirState : PlayerState
{
    private const string AirKey = "Air";
    private const string JumpKey = "IsJumping";

    private IInput _input;
    private PlayerStateController _stateController;

    protected float Speed => _player.GravityForce;
    protected float RotateSpeed => _player.RotateSpeed;
    protected float GravityForce => _player.GravityForce;

    protected Vector3 _velocity;

    public InAirState(Player player) : base(player)
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

        _player.View.SetAnimate(JumpKey, true);
    }

    public override void OnExit()
    {
        Unregister();

        if (_player == null)
            return;

        _player.View.SetAnimate(JumpKey, false);
    }

    private void Register()
    {
        _input.ClickButtonMove += OnClickMove;
        _input.StopMove += OnStopMove;
    }
    private void Unregister()
    {
        _input.ClickButtonMove -= OnClickMove;
        _input.StopMove -= OnStopMove;
    }

    public override void LogicUpdate()
    {
        if (_player == null)
            return;

        if (_player.IsGrounded)
            _stateController.SetState(PlayerStateType.Landed);
    }

    private void OnClickMove(Vector3 direction)
    {
        if (_player == null)
            return;

        _player.Move(direction);
    }

    private void OnStopMove()
    {
        if (_player == null)
            return;

        Vector3 direction = Vector3.zero;

        _player.Move(direction);

        _stateController.SetState(PlayerStateType.Idle);
    }

    public override void Dispose() => OnExit();
}
