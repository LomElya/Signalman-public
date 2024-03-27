public class SelectedButtonManager : MainButtonManager<SettingPanel>
{
    protected override void OnClick(SettingPanel obj, MainButton button)
    {
        base.OnClick(obj, button);

        foreach (var s in Settings)
        {
            s.Button.Unselect();
            s.Obj.Hide();
        }

        obj.Show();
        button.Select();
    }

    public void Show()
    {
        Settings[0].OnClick(Settings[0].Button);
    }
}
