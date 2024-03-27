using System;

public interface IJumpHandler
{
    event Action Jumping;
    event Action OnLanded;

    bool IsLanded { get; }

    void Register(IJumping jumping);
    void Unregister();

    void SetPaused(bool isPaused);
}
