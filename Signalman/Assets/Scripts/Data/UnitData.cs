using UnityEngine;

[System.Serializable]
public class UnitData
{
    [field: SerializeField] public UnitType Type { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Range(1, 10)] public float MaxJumpHeight { get; private set; }
    [field: SerializeField, Range(1, 100)] public float MaxHealth { get; private set; }
    [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 1)] public float RotateSpeed { get; private set; }
}
