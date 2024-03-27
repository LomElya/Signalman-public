using UnityEngine;
using Zenject;

public class PlayerStateFabric
{
    private DiContainer _container;

    private Player _player;

    public void Init(Player player) => _player = player;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    public PlayerState CreatePlayerState(PlayerStateType type)
    {
        PlayerState playerState;

        switch (type)
        {
            case PlayerStateType.Idle:
                playerState = new IdleState(_player);
                break;

            case PlayerStateType.Run:
                playerState = new RunState(_player);
                break;

            case PlayerStateType.Jump:
                playerState = new JumpState(_player);
                break;

            case PlayerStateType.InAir:
                playerState = new InAirState(_player);
                break;

            case PlayerStateType.Landed:
                playerState = new LandedState(_player);
                break;

            default:
                playerState = new IdleState(_player);
                break;
        }

        _container.Inject(playerState);
        return playerState;
    }
}
