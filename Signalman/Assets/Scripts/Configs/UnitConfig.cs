using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Config/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [field: SerializeField] public UnitData Data;
}
