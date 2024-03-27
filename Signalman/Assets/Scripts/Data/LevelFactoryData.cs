using UnityEngine;

[System.Serializable]
public class LevelFactoryData
{
    [field: SerializeField] public LevelConfig Config { get; private set; }
}
