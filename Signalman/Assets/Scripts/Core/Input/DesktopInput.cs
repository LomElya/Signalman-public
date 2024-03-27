using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ILateTickable, IPause
{
    public event Action<Vector3> ClickButtonMove;
    public event Action StopMove;
    public event Action ClickButtonJump;
    public event Action StopJump;
    public event Action ClickInteractButton;
    public event Action ClickPauseButton;

    public bool LastFrameMoving { get; private set; }
    public bool LastFrameInteract { get; private set; }

    public float Horizontal => Input.GetAxisRaw(Constants.Buttons.HORIZONTAL);
    public float Vertical => Input.GetAxisRaw(Constants.Buttons.VERTICAL);
    public Vector2 Direction { get { return new Vector3(Horizontal, Vertical); } }

    public bool IsMooving => Input.GetButton(Constants.Buttons.HORIZONTAL) || Input.GetButton(Constants.Buttons.VERTICAL);
    public bool IsJumping => Input.GetButton(Constants.Buttons.JUMP);

    public bool IsInteracting => Input.GetButtonDown(Constants.Buttons.INTERACT);
    public bool IsPauseButton => Input.GetButtonDown(Constants.Buttons.PAUSE);

    public bool StoppedMoving => !IsMooving && LastFrameMoving;
    public bool StoppedJumping => !IsJumping;

    private PauseHandler _pauseHandler;
    private bool _isPaused;

    public DesktopInput(PauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }

    public void Tick()
    {
        if (_isPaused)
            return;

        Moving();
        Jumping();
        Interacted();
        Pause();
    }

    private void Moving()
    {
        if (StoppedMoving)
            StopMove?.Invoke();

        else if (IsMooving)
        {
            Vector3 newDirection = new Vector3(Direction.x, 0, Direction.y);
            ClickButtonMove?.Invoke(newDirection);
        }
    }

    private void Jumping()
    {
        if (IsJumping)
            ClickButtonJump?.Invoke();
        else if (StoppedJumping)
            StopJump?.Invoke();
    }

    private void Interacted()
    {
        if (IsInteracting && !LastFrameInteract)
            ClickInteractButton?.Invoke();
    }

    private void Pause()
    {
        if (IsPauseButton)
            ClickPauseButton?.Invoke();
    }

    public void LateTick()
    {
        LastFrameMoving = IsMooving;
        LastFrameInteract = IsInteracting;
    }

    public void SetPause(bool isPaused) => _isPaused = isPaused;
}
