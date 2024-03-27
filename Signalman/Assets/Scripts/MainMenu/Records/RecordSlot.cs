using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecordSlot : MonoBehaviour, IPointerDownHandler
{
    private const string KeyWin = "Win";
    private const string KeyLose = "Lose";

    [SerializeField] private RecordSlotAnimation _animation;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _secondsText;
    [SerializeField] private TMP_Text _finishedText;
    [SerializeField] private TMP_Text _dieText;

    private bool _isOpen;

    public void Init(LevelData data)
    {
        _isOpen = false;

        _levelText.text = $"Level {data.ID + 1}";

        ShowSeconds(data);
        ShowInfo(data);
    }

    private void ShowSeconds(LevelData data)
    {
        float value;

        PlayerExtensions.Load(data.NAME_SCENE, out value);

        _secondsText.text = value.ToString();
    }

    private void ShowInfo(LevelData data)
    {
        int value;
        PlayerExtensions.Load(data.NAME_SCENE + KeyWin, out value);
        _finishedText.text = value.ToString();

        PlayerExtensions.Load(data.NAME_SCENE + KeyLose, out value);
        _dieText.text = value.ToString();
    }

    public async void OnPointerDown(PointerEventData eventData)
    {
        if (!_isOpen)
        {
            _isOpen = true;
            await _animation.Play();
        }
        else
        {
            _isOpen = false;
            _animation.Resume();
        }
    }
}
