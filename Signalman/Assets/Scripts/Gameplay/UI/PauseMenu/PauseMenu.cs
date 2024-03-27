using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : UIState
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    public static UniTask<Disposable<PauseMenu>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<PauseMenu>(AssetsConstants.PauseMenu);
    }

    private void OnContinueButtonClick()
    {
        _taskCompletion.TrySetResult(true);
    }

    private void OnQuitButtonClick()
    {
        _taskCompletion.TrySetResult(false);
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _continueButton.onClick.AddListener(OnContinueButtonClick);
        _exitButton.onClick.AddListener(OnQuitButtonClick);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
        _exitButton.onClick.RemoveListener(OnQuitButtonClick);
    }
}
