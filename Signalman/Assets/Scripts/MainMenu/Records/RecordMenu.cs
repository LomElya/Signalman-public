using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecordMenu : UIState
{
    [SerializeField] private RecordPanel _panel;

    private LevelFactory _levelFactory;

    [Inject]
    private void Construct(LevelFactory levelFactory)
    {
        _levelFactory = levelFactory;
    }

    protected override void OnShow()
    {
        base.OnShow();

        _panel.Show(_levelFactory);
    }
}
