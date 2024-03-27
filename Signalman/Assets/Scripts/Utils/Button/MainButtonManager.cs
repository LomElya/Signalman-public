using System;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonManager<T> : MonoBehaviour
{
    public event Action<T> OnClickButton;

    [field: SerializeField] public List<MenuButtonSetting> Settings { get; private set; }

    public void Init()
    {
        foreach (var setting in Settings)
        {
            setting.Init();
            setting.Click += OnClick;
        }
    }

    public void Hide()
    {
      /*   foreach (var setting in Settings)
        {
            setting.Click -= OnClick;
        } */
    }

    protected virtual void OnClick(T obj, MainButton button) => OnClickButton?.Invoke(obj);

    [Serializable]
    public class MenuButtonSetting
    {
        public event Action<T, MainButton> Click;
        [field: SerializeField] public MainButton Button { get; private set; }
        [field: SerializeField] public T Obj;

        public void Init() => Button.Click += OnClick;
       // public void Hide() => Button.Click -= OnClick;
        public void OnClick(MainButton button) => Click.Invoke(Obj, button);
        private void OnDisable() => Button.Click -= OnClick;
    }
}
