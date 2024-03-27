using UnityEngine;

public class Interacter : MonoBehaviour
{
    private IInput _input;

    private InteractableZone _interactable;

    private bool _isPause;

    public void Init(IInput input)
    {
        _input = input;

        _input.ClickInteractButton += OnOnteractClickButton;
    }

    public void Enter(InteractableZone interactable)
    {
        if (_isPause)
            return;

        _input.ClickInteractButton += OnOnteractClickButton;
        _interactable = interactable;
    }

    public void Exit()
    {
        _input.ClickInteractButton -= OnOnteractClickButton;
        _interactable = null;
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;
    }

    private void OnOnteractClickButton()
    {
        if (_interactable == null)
            return;

        _interactable.Interact();
        //_input.ClickInteractButton -= OnOnteractClickButton;
    }
}
