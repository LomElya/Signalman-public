using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        Subscribe();
        OnShow();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        Unsubscribe();
        OnHide();
    }

    protected virtual void OnShow() { }
    protected virtual void OnHide() { }

    protected virtual void Subscribe() { }
    protected virtual void Unsubscribe() { }

    private void OnEnable() => Enable();
    private void OnDisable() => Disable();

    protected virtual void Enable() { }
    protected virtual void Disable() { }
}
