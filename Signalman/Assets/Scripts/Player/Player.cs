using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IMovable, IJumping, IPause
{
    [SerializeField] private float _gravityForce = 9.8f;
    [SerializeField] private PlayerView _view;
    [SerializeField] private Interacter _interacter;
    [SerializeField] private GroundChecker _groundChecher;

    private PauseHandler _pauseHandler;

    private PlayerStateController _stateController;

    private float _maxJumpHeight;
    private float _maxJumpTime;
    private float _jumpVelocity;

    private float _speed;
    private float _rotateSpeed;

    private bool _isPaused;

    public PlayerView View => _view;

    public float GravityForce => _gravityForce;

    public float Speed => _speed;
    public float RotateSpeed => _rotateSpeed;
    public Transform Transform => transform;

    public float MaxJumpHeight => _maxJumpHeight;
    public float MaxJumpTime => _maxJumpTime;
    public float JumpVelocity => _jumpVelocity;

    public bool IsGrounded => _groundChecher.IsGrounded;

    /// 
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    private Vector3 _velocity;
    public Vector3 Velocity { get => _velocity; set => _velocity = value; }

    private void Awake() => _rigidbody ??= GetComponent<Rigidbody>();


    [Inject]
    private void Construct(IInput input, PauseHandler pauseHandler, PlayerStateController stateController)
    {
        _pauseHandler = pauseHandler;

        _stateController = stateController;

        _interacter.Init(input);
    }

    public void Init(PlayerData playerData)
    {
        _maxJumpHeight = playerData.MaxJumpHeight;
        _maxJumpTime = playerData.MaxJumpTime;

        _speed = playerData.Speed;
        _rotateSpeed = playerData.RotateSpeed;

        _view.SetAudioClips(playerData.StepSounds, playerData.JumpSounds, playerData.LandSounds);

        float maxHeightTime = MaxJumpTime / 2f;
        _gravityForce = (2 * MaxJumpHeight) / Mathf.Pow(maxHeightTime, 2);

        _jumpVelocity = (2 * MaxJumpHeight) / maxHeightTime;
    }

    public void Register()
    {
        _pauseHandler.Add(this);
        OnStart();
    }

    private async void OnStart()
    {
        await UniTask.WaitForSeconds(1f);

        _stateController.Init(this);
    }

    public void Unregister()
    {
        // _view.Unregister();
        _stateController.UnRegister();
        _pauseHandler.Remove(this);
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;

        // Rigidbody.isKinematic = isPaused;

        _interacter.SetPause(isPaused);

        Rigidbody.velocity = Vector3.zero;
        _view.SetAnimate("Speed", Rigidbody.velocity.magnitude);

        /*   _movementHandler.SetPaused(isPaused);
          _jumpHandler.SetPaused(isPaused); */
    }

    public void Move(Vector3 direction)
    {
        if (_isPaused)
            return;

        Vector3 move = Rotate(direction);

        Transform.LookAt(Transform.position + move);

        move = move.normalized;
        move *= Speed;

        Rigidbody.velocity = new Vector3(move.x, Rigidbody.velocity.y, move.z);
    }

    private Vector3 Rotate(Vector3 direction)
    {
        Vector3 forward = Transform.forward;

        if (Vector3.Angle(forward, direction) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(forward, direction, RotateSpeed, 0);
            //_player.Transform.rotation = Quaternion.LookRotation(newDirection);

            return newDirection;
        }

        return direction;
    }

    private void OnDisable() => Unregister();
}
