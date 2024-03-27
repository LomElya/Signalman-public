using UnityEngine;

[System.Serializable]
public class UnitFactoryData
{
    [field: SerializeField] public Unit Prefab { get; private set; }
    [field: SerializeField] public UnitConfig Config { get; private set; }
}
