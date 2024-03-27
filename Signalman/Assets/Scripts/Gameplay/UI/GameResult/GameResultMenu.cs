using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultMenu : UIState
{
    [SerializeField] private GameResultIntroAnimation _introAnimation;

    [Header("Buttons")]
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    // private Canvas _canvas;

    private GameResultType _resultType;

    private Action _onQuit;
    private Action _callback;

    public static UniTask<Disposable<GameResultMenu>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<GameResultMenu>(AssetsConstants.GameResultMenu);
    }

    public async UniTask<bool> Show(GameResultType resultType, Action callback, Action onQuit, string description = "")
    {
        _callback = callback;
        _onQuit = onQuit;

        _description.text = description;

        _resultType = resultType;

        await Show();

        var result = await _taskCompletion.Task;

        return result;
    }

    protected async override void OnShow()
    {
        base.OnShow();

        OnInteractableButton(false);

        await _introAnimation.Play(_resultType);

        OnInteractableButton(true);
    }

    private void OnInteractableButton(bool isInteractable)
    {
        _nextLevelButton.interactable = isInteractable;
        _restartButton.interactable = isInteractable;
        _quitButton.interactable = isInteractable;
    }

    private void OnNextLevelButtonClick()
    {
        _callback?.Invoke();
        _canvas.enabled = false;

        _taskCompletion.TrySetResult(true);
    }

    private void OnRestartButtonClick()
    {
        _callback?.Invoke();
        _canvas.enabled = false;

        _taskCompletion.TrySetResult(true);
    }

    private void OnQuitButtonClick()
    {
        _onQuit?.Invoke();
        _canvas.enabled = false;

        _taskCompletion.TrySetResult(false);
    }

    protected override void Subscribe()
    {
        base.Unsubscribe();

        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _quitButton.onClick.RemoveListener(OnQuitButtonClick);
    }
}
