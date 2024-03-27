using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFactory", menuName = "Factory/PlayerFactory")]
public class PlayerFactory : GameObjectFactory
{
    [field: SerializeField] public List<PlayerFactoryData> _datas;

    public Player Get(int id)
    {
        PlayerFactoryData data = GetData(id);

        Player player = CreateGameObjectInstance(data.Prefab);

        if (player == null)
        {
            Debug.LogErrorFormat($"Префаб с id ({id}) не найден");
            return null;
        }

        player.Init(data.Config.Data);

        return player;
    }

    public PlayerFactoryData GetData(int id)
    {
        PlayerFactoryData data = _datas.FirstOrDefault(x => x.Config.Data.ID == id);

        if (data == null)
        {
            Debug.LogErrorFormat($"Игрок с ID ({id}) не найден");
            return null;
        }

        return data;
    }

    public void Delete(Player player)
    {
        player.Unregister();
        Destroy(player.gameObject);
    }
}
