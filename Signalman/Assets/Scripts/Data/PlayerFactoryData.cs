using UnityEngine;

[System.Serializable]
public class PlayerFactoryData
{
    [field: SerializeField] public PlayeConfig Config { get; private set; }
    [field: SerializeField] public Player Prefab { get; private set; }
}