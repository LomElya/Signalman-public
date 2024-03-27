using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public event Action<MainButton> Click;

    [SerializeField] private List<MainButton> _buttons;

    //  private void Awake() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    private void OnClick(MainButton button)
    {
        //Debug.Log(button.ID);
        Click?.Invoke(button);
    }

    public void Subscribe()
    {
        for (int i = 0; i <= _buttons.Count - 1; i++)
        {
            int id = i + 1;
            _buttons[i].On();
            _buttons[i].SetID(id);
            _buttons[i].Click += OnClick;
        }
    }

    public void Unsubscribe()
    {
        foreach (var button in _buttons)
        {
            button.Off();
            button.Click -= OnClick;
        }
    }
}
