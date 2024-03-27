using System;
using UnityEngine;

public class Level
{
    public event Action<int> LevelChange;

    public int CurrentLevelID { get; private set; }

    public bool IsGameLoadNotMenu = true;

    public Level()
    {
        CurrentLevelID = 0;
        IsGameLoadNotMenu = true;
    }

    public void ChangeLevel(int id)
    {
        CurrentLevelID = id;

        LevelChange?.Invoke(CurrentLevelID);
    }

    public void NextLevel()
    {
        CurrentLevelID++;

        Debug.Log($"Текущий уровень {CurrentLevelID}");

        LevelChange?.Invoke(CurrentLevelID);
    }
}
