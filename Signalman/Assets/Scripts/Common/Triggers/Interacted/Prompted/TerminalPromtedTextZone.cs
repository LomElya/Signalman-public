using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TerminalPromtedTextZone : MonoBehaviour
{
  protected const string KeyTooltips = "Tooltips";

  [SerializeField, TextArea] protected List<string> _description;

  [Inject] private SignalBus _signalBus;

  public async void OnInteract()
  {
    bool isConfirmed = PlayerExtensions.Load(KeyTooltips);

    if (isConfirmed)
      return;

    var promtedAlert = await MessagePanel.Load();

    _signalBus.Fire(new PauseSignal(true));

    await promtedAlert.Value.Show(_description);

    _signalBus.Fire(new PauseSignal(false));

    promtedAlert?.Dispose();
  }
}
