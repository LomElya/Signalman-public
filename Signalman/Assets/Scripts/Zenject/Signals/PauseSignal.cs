using UnityEngine;

public class PauseSignal : MonoBehaviour
{
    public bool IsPause { get; private set; }

    public PauseSignal(bool isPause)
    {
        IsPause = isPause;
    }
}
