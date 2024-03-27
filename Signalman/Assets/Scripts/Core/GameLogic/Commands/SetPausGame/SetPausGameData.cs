public class SetPausGameData : CommandData
{
    public bool IsPaused { get; private set; }

    public SetPausGameData(bool isPaused)
    {
        IsPaused = isPaused;
    }
}
