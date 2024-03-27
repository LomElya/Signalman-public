public class SettingsButton : MainButton
{
    protected async override void OnClick()
    {
        base.OnClick();

        var settingsMenu = await SettingsMenu.Load();

        await settingsMenu.Value.Show();

        settingsMenu.Dispose();
    }
}
