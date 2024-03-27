using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class AutorsMenu : UIState
{
    [SerializeField] private Button _buttonBack;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void Start()
    {
        _buttonBack.onClick.AddListener(Hide);
        _canvas.enabled = false;
    }

    public static UniTask<Disposable<AutorsMenu>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<AutorsMenu>(AssetsConstants.AutorsMenu);
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _buttonBack.onClick.AddListener(Hide);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _buttonBack.onClick.RemoveListener(Hide);
    }
    private void Hide() => _taskCompletion.TrySetResult(false);
}
