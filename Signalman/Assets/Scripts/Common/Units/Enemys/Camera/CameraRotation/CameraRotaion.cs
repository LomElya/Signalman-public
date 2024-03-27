using UnityEngine;
using Zenject;

public class CameraRotaion : CameraEnemy, IPause
{
    [SerializeField] private bool _reversePath;
    [SerializeField] private float _speedRotatio;
    [SerializeField] private GameObject _head;

    private PauseHandler _pauseHandler;

    private bool _isPaused;

    [Inject]
    private void Construct(PauseHandler pauseHandler) => _pauseHandler = pauseHandler;
    private void Start() => _pauseHandler.Add(this);

    public void SetPause(bool isPaused) => _isPaused = isPaused;

    private void Update()
    {
        if (_isPaused)
            return;

        float angle = _head.transform.eulerAngles.z;

        float direction = _speedRotatio * Time.deltaTime;

        if (_reversePath)
            direction = -direction;

        _head.transform.Rotate(0f, 0f, direction);
    }
}
