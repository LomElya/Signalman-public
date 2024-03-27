using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPanel : SettingPanel
{
    [SerializeField] private Slider _generalSound;
    [SerializeField] private Slider _musicSound;
    [SerializeField] private Slider _effectSound;

    protected override void OnShow()
    {
        Subscribe();

        _generalSound.value = AudioUtility.GetMasterVolume();
        _musicSound.value = AudioUtility.GetVolume(AudioUtility.AudioGroups.AmbientVol);
        _effectSound.value = AudioUtility.GetVolume(AudioUtility.AudioGroups.EffectVol);
    }

    protected override void OnHide()
    {
        Unsubscribe();
    }

    protected override void Disable() => OnHide();

    protected override void Subscribe()
    {
        _generalSound.onValueChanged.AddListener(GeneralSliderChange);
        _musicSound.onValueChanged.AddListener(MusicSliderChange);
        _effectSound.onValueChanged.AddListener(EffectSliderChange);
    }

    protected override void Unsubscribe()
    {
        _generalSound.onValueChanged.RemoveListener(GeneralSliderChange);
        _musicSound.onValueChanged.RemoveListener(MusicSliderChange);
        _effectSound.onValueChanged.RemoveListener(EffectSliderChange);
    }

    private void GeneralSliderChange(float value) => AudioUtility.SetMasterVolume(value);
    private void MusicSliderChange(float value) => AudioUtility.SetVolume(AudioUtility.AudioGroups.AmbientVol, value);
    private void EffectSliderChange(float value) => AudioUtility.SetVolume(AudioUtility.AudioGroups.EffectVol, value);
}
