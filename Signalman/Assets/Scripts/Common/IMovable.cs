using UnityEngine;

public interface IMovable
{
    float Speed { get; }
    float RotateSpeed { get; }

    Transform Transform { get; }

    Rigidbody Rigidbody { get; }
}
