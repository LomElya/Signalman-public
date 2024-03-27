using UnityEngine;

[CreateAssetMenu(fileName = "PlayeConfig", menuName = "Config/PlayeConfig")]
public class PlayeConfig : ScriptableObject
{
    [field: SerializeField] public PlayerData Data;
}
