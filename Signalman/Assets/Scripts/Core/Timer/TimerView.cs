using TMPro;
using UnityEngine;
using Zenject;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private float _minuts;
    private float _seconds;

    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        _timer.TimeUpdated += OnTimeUpdate;
    }

    private void OnTimeUpdate(float seconds)
    {
        _minuts = Mathf.FloorToInt(seconds / 60);
        _seconds = Mathf.FloorToInt(seconds % 60);

        _timerText.text = string.Format("{0:00} : {1:00}", _minuts, _seconds);
    }

    private void OnDisable() => _timer.TimeUpdated -= OnTimeUpdate;
}
