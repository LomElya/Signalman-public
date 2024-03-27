using System;

public abstract class PlayerState : IDisposable
{
    protected Player _player;

    public PlayerState(Player player) => _player = player;

    public abstract void OnEnter();
    public abstract void OnExit();

    public virtual void OnUpdate() { }
    public virtual void LogicUpdate() { }
    public virtual void OnFixedUpdate() { }

    public virtual void Dispose() { }
}
