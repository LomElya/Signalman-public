using System;
using UnityEngine;

public interface IMovementHandler
{
    event Action<Vector3> DirectionMove;

    float Speed { get; }

    void Register(IMovable movable);
    void Unregister();

    void SetPaused(bool isPaused);
}
