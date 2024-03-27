using Cysharp.Threading.Tasks;
using UnityEngine;

public class SettingsMenu : UIState
{
    [SerializeField] private SelectedButtonManager _manager;
    [SerializeField] private MainButton _buttonBack;

    public static UniTask<Disposable<SettingsMenu>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<SettingsMenu>(AssetsConstants.SettingsMenu);
    }

    protected override void OnShow()
    {
        base.OnShow();

        _manager.Init();
        _manager.Show();
    }

    protected override void Unload()
    {
        base.Unload();

        _taskCompletion.TrySetResult(false);
        _manager.Hide();
    }

    private void OnClickButton(SettingPanel panel)
    {

    }

    private void OnClickBackButton(MainButton button) => Hide();


    protected override void Subscribe()
    {
        base.Subscribe();

        _buttonBack.Click += OnClickBackButton;
        _manager.OnClickButton += OnClickButton;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _buttonBack.Click -= OnClickBackButton;
        _manager.OnClickButton -= OnClickButton;
    }
}
