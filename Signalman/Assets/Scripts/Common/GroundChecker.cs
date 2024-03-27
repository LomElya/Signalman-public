using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGrounded => IsGround();

    private CapsuleCollider _collider;

    public bool IsGround() => Physics.Raycast(_groundCheck.position, Vector3.down, _groundDistance, _groundMask);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);
    }
}
