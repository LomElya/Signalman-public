using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PromtedTextZone : PromtedZone
{
    [SerializeField, TextArea] protected List<string> _description;

    protected override async void OnInteract()
    {
        var promtedAlert = await MessagePanel.Load();

        _signalBus.Fire(new PauseSignal(true));

        await promtedAlert.Value.Show(_description);

        _signalBus.Fire(new PauseSignal(false));

        promtedAlert?.Dispose();
    }
}
