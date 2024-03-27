using UnityEngine;
using UnityEngine.UI;

public class GameSettingPanel : SettingPanel
{
    private const string KeyTooltips = "Tooltips";

    [SerializeField] private MainButton _resetButton;
    [SerializeField] private Toggle _disableTooltips;


    protected override void OnShow()
    {
        base.OnShow();

        bool isConfirmed = PlayerExtensions.Load(KeyTooltips);
        _disableTooltips.isOn = isConfirmed;
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    private async void OnResetButtonClick(MainButton _)
    {
        var alertPopup = await PopupAlert.Load();
        bool isConfirmed = await alertPopup.Value.SetDescription("Вы точно хотите сбросить весь прогресс?\nНастройки так же сбросятся.");

        if (isConfirmed)
            PlayerExtensions.ResetSave();

        alertPopup.Dispose();
    }

    private void OnDisableTooltips(bool isDisable)
    {
        Debug.Log(" Сохраняю" + " " + isDisable.ToString());
        PlayerExtensions.Save(KeyTooltips, isDisable);

        bool isConfirmed = PlayerExtensions.Load(KeyTooltips);

        Debug.Log($" Получаю {isConfirmed}");
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _resetButton.Click += OnResetButtonClick;

        _disableTooltips.onValueChanged.AddListener(OnDisableTooltips);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        _resetButton.Click -= OnResetButtonClick;

        _disableTooltips.onValueChanged.RemoveListener(OnDisableTooltips);
    }
}
