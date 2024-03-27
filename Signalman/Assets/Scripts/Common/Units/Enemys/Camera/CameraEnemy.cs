using UnityEngine;
using Zenject;

public class CameraEnemy : MonoBehaviour
{
    [SerializeField] private DetectionModule _detectionModule;
    [SerializeField] private CameraRay _ray;

    private SignalBus _signalBus;

    public bool IsIncluded { get; private set; }

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        IsIncluded = true;
        Subscribe();

        if (_ray != null)
            _ray.SetActive(IsIncluded);
    }

    public void OffCamera()
    {
        if (!IsIncluded)
            return;

        IsIncluded = false;

        if (_ray != null)
            _ray.SetActive(IsIncluded);

        Debug.Log("Отключить камеру");
    }

    private void OnDetectPlayer()
    {
        Debug.Log("нашелся");

        if (!IsIncluded)
            return;

        Unubscribe();

        Debug.Log("ПОПАВСЯ КУСАЧИЙ");
        _signalBus.Fire(new GameLoseSignal());
    }

    private void Subscribe() => _detectionModule.DetectPlayer += OnDetectPlayer;
    private void Unubscribe() => _detectionModule.DetectPlayer -= OnDetectPlayer;

    private void OnDisable() => Unubscribe();
}
