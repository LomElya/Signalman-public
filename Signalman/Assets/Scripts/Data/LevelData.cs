using UnityEngine;


[System.Serializable]
public class LevelData
{
    [field: SerializeField] public int ID;
    [field: SerializeField] public string NAME_SCENE;
    [field: SerializeField, Range(20, 600)] public float MaxTime;
}
