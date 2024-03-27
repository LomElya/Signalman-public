using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public sealed class GameProcessor : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private Transform _startPositionCamara;

    private PlayerFactory _playerFactory;
    private Timer _timer;

    private LevelData _levelData;

    private Player _player;

    private UniTaskCompletionSource _startCompletion;
    private UniTaskCompletionSource _stopCompletion;

    private Camera _mainCamera;

    private DiContainer _diContainer;

    [Inject]
    private void Construct(PlayerFactory playerFactory, DiContainer diContainer, Timer timer)
    {
        _playerFactory = playerFactory;
        _diContainer = diContainer;
        _timer = timer;
    }

    private void Awake()
    {
        _startPositionCamara = _virtualCamera.transform;
        _mainCamera = Camera.main;
    }

    public UniTask StartGame(LevelData levelData)
    {
        _mainCamera.transform.position = _startPositionCamara.transform.position;
        _virtualCamera.transform.position = _startPositionCamara.transform.position;

        _startCompletion = new UniTaskCompletionSource();

        _levelData = levelData;

        _timer.Stop();
        _timer.Start(_levelData.MaxTime);

        SpawnPlayer();

        return _startCompletion.Task;
    }

    public UniTask EndGame()
    {
        _stopCompletion = new UniTaskCompletionSource();
        _timer.Stop();

        _virtualCamera.Follow = null;

        _playerFactory.Delete(_player);

        UnFollowCamera();

        return _stopCompletion.Task;
    }

    private void SpawnPlayer()
    {
        if (_player != null)
            _playerFactory.Delete(_player);

        _player = _playerFactory.Get(0);

        _diContainer.Inject(_player);

        _player.Register();

        FollowCavera();
    }

    private void FollowCavera()
    {
        _virtualCamera.Follow = _player.gameObject.transform;
        _virtualCamera.LookAt = _player.gameObject.transform;

        _startCompletion?.TrySetResult();
    }

    public void UnFollowCamera()
    {
        _virtualCamera.Follow = null;
        _virtualCamera.LookAt = null;

        _stopCompletion?.TrySetResult();
    }
}
