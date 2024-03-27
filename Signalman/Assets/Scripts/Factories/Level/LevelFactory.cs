using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelFactory", menuName = "Factory/LevelFactory", order = 0)]
public class LevelFactory : ScriptableObject
{
    [SerializeField] private List<LevelFactoryData> _levelDatas;
    public List<LevelFactoryData> LevelDatas => _levelDatas;

    public int MaxCountLevel => _levelDatas.Count;

    public string GetNameScene(int id)
    {
        LevelFactoryData data = GetFactoryData(id);

        return data.Config.Data.NAME_SCENE;
    }

    public LevelFactoryData GetFactoryData(int id)
    {
        LevelFactoryData data = _levelDatas.FirstOrDefault(x => x.Config.Data.ID == id);

        if (data == null)
        {
            Debug.LogError($"Уровень с ID {id} не найден");
            return null;
        }

        return data;
    }

    public LevelData GetData(int id)
    {
        LevelFactoryData data = GetFactoryData(id);

        return data.Config.Data;
    }
}
