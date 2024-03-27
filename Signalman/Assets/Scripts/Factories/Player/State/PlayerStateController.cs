using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStateController : ITickable, IFixedTickable
{
    private Dictionary<PlayerStateType, PlayerState> _states = new();

    public PlayerState CurrentState { get; private set; }

    private Player _player;

    private PlayerStateFabric _fabric;

    [Inject]
    private void Construct(PlayerStateFabric fabric)
    {
        _fabric = fabric;
    }

    public void Init(Player player)
    {
        // _fabric.Init(_animator, movable, jumping);

        _player = player;

        _fabric.Init(_player);

        CurrentState?.OnExit();

        CurrentState = _fabric.CreatePlayerState(PlayerStateType.Idle);

        CurrentState.OnEnter();
    }

    public void UnRegister()
    {
        _player = null;
        _states.Clear();
        _states = new();
    }

    public void SetState(PlayerStateType type)
    {
        if (_player == null)
            return;

        PlayerState mewPlayerState;

        if (_states.ContainsKey(type))
            mewPlayerState = _states[type];
        else
        {
            mewPlayerState = _fabric.CreatePlayerState(type);
            _states.Add(type, mewPlayerState);
        }

        CurrentState.OnExit();

        CurrentState = mewPlayerState;

        CurrentState.OnEnter();
    }

    public void Tick()
    {
        if (CurrentState == null)
            return;

        CurrentState.OnUpdate();

        CurrentState.LogicUpdate();
    }

    public void FixedTick()
    {
        if (CurrentState == null)
            return;

        CurrentState.OnFixedUpdate();
    }
}
