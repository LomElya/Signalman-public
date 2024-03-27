public class SelectedButton : MainButton
{
    public override void Select() => SetColor(_pressedColor);
    public override void Unselect() => SetColor(_normalColor);


    protected override void OnPointEnter() { }
    protected override void PoinExit() { }
    protected override void PoinDown() { }
}
