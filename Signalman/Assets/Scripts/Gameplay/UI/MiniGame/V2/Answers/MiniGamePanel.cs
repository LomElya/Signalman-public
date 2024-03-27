using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MiniGamePanel : MonoBehaviour
{
    public event Action<bool> Finished;

    [SerializeField] protected Transform _contentParent;
    [SerializeField] private MiniGameSlot _slotPrefab;

    private List<MiniGameSlot> _slots = new();

    private bool _isFinished;


    private int _currentID = 0;

    public void Show()
    {
        Clear();

        int randomRange = Random.Range(4, 5);

        for (int i = 0; i <= randomRange; i++)
        {
            MiniGameSlot slot = Instantiate(_slotPrefab, _contentParent);

            slot.Init(new AnswerGenerator());

            _slots.Add(slot);
        }
    }

    public void ClickButton(int id)
    {
        MiniGameSlot slot = _slots[_currentID];
        bool isCorrectAnswer = slot.IsCorrectAnswer(id);

        if (isCorrectAnswer)
            _currentID++;
        else
        {
            _isFinished = true;
            Finished?.Invoke(false);
        }

        CheckFinish();
    }

    private void CheckFinish()
    {
        if (_isFinished)
            return;

        if (_currentID != _slots.Count)
            return;

        _isFinished = true;

        Finished?.Invoke(true);
    }

    private void Clear()
    {
        foreach (var slot in _slots)
            Destroy(slot.gameObject);

        _slots?.Clear();
        _isFinished = false;
        _currentID = 0;
    }
}
