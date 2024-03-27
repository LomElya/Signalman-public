using Cysharp.Threading.Tasks;
using UnityEngine;

public class MiniGameMenuV2 : UIState
{
    [SerializeField] private MiniGamePanel _panel;
    [SerializeField] private MainButton _buttonExit;
    [SerializeField] private ButtonManager _buttonManager;

    public bool IsExit { get; private set; }

    protected override void OnShow()
    {
        base.OnShow();
        //gameObject.SetActive(true);
        _panel.Show();
    }

    protected override void Unload()
    {
        _canvas.enabled = false;
        //  gameObject.SetActive(false);
    }

    private async void OnFinished(bool isCorrect)
    {
        IsExit = false;

        await UniTask.WaitForSeconds(0.5f);

        if (isCorrect)
            _taskCompletion.TrySetResult(true);
        else
            _taskCompletion.TrySetResult(false);
    }

    private void OnClickButton(MainButton button)
    {
        int id = button.ID;

        _panel.ClickButton(id);
    }

    private void OnClickButtonExit(MainButton button)
    {
        IsExit = true;
        _taskCompletion.TrySetResult(false);
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _buttonManager.Subscribe();

        _panel.Finished += OnFinished;

        _buttonExit.Click += OnClickButtonExit;
        _buttonManager.Click += OnClickButton;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _buttonManager.Unsubscribe();

        _panel.Finished -= OnFinished;

        _buttonExit.Click -= OnClickButtonExit;
        _buttonManager.Click -= OnClickButton;
    }
}
