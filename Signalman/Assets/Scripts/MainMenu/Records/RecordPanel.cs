using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPanel : MonoBehaviour
{
    [SerializeField] private RecordSlot _slotPrefab;
    [SerializeField] private Transform _parent;

    private List<RecordSlot> _slots = new();

    public void Show(LevelFactory factory)
    {
        Clear();

        foreach (var factoryData in factory.LevelDatas)
        {
            LevelData data = factoryData.Config.Data;
            RecordSlot slot = Instantiate(_slotPrefab, _parent);

            slot.Init(data);
            _slots.Add(slot);
        }
    }

    private void Clear()
    {
        foreach (RecordSlot slot in _slots)
            Destroy(slot.gameObject);

        _slots?.Clear();
    }
}
