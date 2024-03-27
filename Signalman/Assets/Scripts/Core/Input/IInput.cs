using System;
using UnityEngine;
using Zenject;

public interface IInput : ITickable, ILateTickable
{
    event Action<Vector3> ClickButtonMove;
    event Action StopMove;
    event Action ClickButtonJump;
    event Action StopJump;
    event Action ClickInteractButton;
    event Action ClickPauseButton;

    bool LastFrameMoving { get; }

    float Horizontal { get; }
    float Vertical { get; }

    Vector2 Direction { get; }

    bool IsMooving { get; }
    bool IsJumping { get; }

    bool StoppedMoving { get; }
    bool StoppedJumping { get; }
}
