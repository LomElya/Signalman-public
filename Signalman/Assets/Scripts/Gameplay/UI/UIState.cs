using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class UIState : MonoBehaviour
{
    [SerializeField] protected Button _outsideClickArea;

    protected Canvas _canvas;

    protected UniTaskCompletionSource<bool> _taskCompletion;
    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;
    }

    public async UniTask<bool> Show()
    {
        _taskCompletion = new UniTaskCompletionSource<bool>();

        Subscribe();
        OnShow();

        var result = await _taskCompletion.Task;

        Unload();
        Unsubscribe();

        return result;
    }

    protected virtual void OnShow() => _canvas.enabled = true;
    protected virtual void Unload() => _canvas.enabled = false;

    protected virtual void Subscribe()
    {
        if (_outsideClickArea != null)
            _outsideClickArea.onClick.AddListener(Hide);
    }
    protected virtual void Unsubscribe()
    {
        if (_outsideClickArea != null)
            _outsideClickArea.onClick.RemoveListener(Hide);
    }

    protected virtual void Hide() => _taskCompletion.TrySetResult(false);


    protected virtual void OnEnable() { }
    protected virtual void OnDisable() => Unsubscribe();
}
