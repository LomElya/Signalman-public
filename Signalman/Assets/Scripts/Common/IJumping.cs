using UnityEngine;

public interface IJumping
{
    float MaxJumpHeight { get; }

    bool IsGrounded { get; }

    Rigidbody Rigidbody { get; }
}
