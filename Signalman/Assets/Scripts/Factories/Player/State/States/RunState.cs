using UnityEngine;
using Zenject;

public class RunState : PlayerState
{
     private const string MoveKey = "Speed";

     private IInput _input;
     private PlayerStateController _stateController;

     protected float Speed => _player.Speed;
     protected float RotateSpeed => _player.RotateSpeed;

     protected Vector3 _velocity;

     public RunState(Player player) : base(player)
     {
     }

     [Inject]
     private void Construct(IInput input, PlayerStateController stateController)
     {
          _input = input;
          _stateController = stateController;
     }

     public override void OnEnter() => Register();
     public override void OnExit()
     {
          Unregister();

          if (_player == null)
               return;

          _player.View.SetAnimate(MoveKey, 0f);
     }

     private void Register()
     {
          _input.ClickButtonMove += OnClickMove;
          _input.StopMove += OnStopMove;

          _input.ClickButtonJump += OnClickJump;
     }

     private void Unregister()
     {
          _input.ClickButtonMove -= OnClickMove;
          _input.StopMove -= OnStopMove;

          _input.ClickButtonJump -= OnClickJump;
     }

     private void OnClickMove(Vector3 direction)
     {
          if (_player == null)
               return;

          _player.Move(direction);
          _player.View.SetAnimate(MoveKey, direction.magnitude);
     }

     private void OnStopMove()
     {
          if (_player == null)
               return;

          Vector3 direction = Vector3.zero;

          _player.Move(direction);
          _player.View.SetAnimate(MoveKey, 0);

          _stateController.SetState(PlayerStateType.Idle);
     }

     private void OnClickJump()
     {
          _stateController.SetState(PlayerStateType.Jump);
     }
     public override void Dispose() => OnExit();
}
