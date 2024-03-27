using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class PopupAlert : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _closeButton;

    private UniTaskCompletionSource<bool> _taskCompletion;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;

        _okButton.onClick.AddListener(OnAccept);
        _cancelButton.onClick.AddListener(OnCancelled);
        _closeButton.onClick.AddListener(OnCancelled);
    }

    public static UniTask<Disposable<PopupAlert>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<PopupAlert>(AssetsConstants.PopupAlert);
    }

    public async UniTask<bool> SetDescription(string text)
    {
        _text.text = text;

        _canvas.enabled = true;

        _taskCompletion = new UniTaskCompletionSource<bool>();
        var result = await _taskCompletion.Task;

        _canvas.enabled = false;

        return result;
    }

    private void OnAccept() => _taskCompletion.TrySetResult(true);
    private void OnCancelled() => _taskCompletion.TrySetResult(false);

    private void OnDisable()
    {
        _okButton.onClick.RemoveListener(OnAccept);
        _cancelButton.onClick.RemoveListener(OnCancelled);
        _closeButton.onClick.RemoveListener(OnCancelled);
    }
}
