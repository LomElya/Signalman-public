using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioUtility
{
    public static AudioManager AudioManager;

    public AudioUtility(AudioManager audioManager) => AudioManager = audioManager;

    public enum AudioGroups
    {
        MasterVolume,
        AmbientVol,
        EffectVol,
    }

    public static void CreateSFX(AudioClip clip, Vector3 position, AudioGroups audioGroup, float spatialBlend,
              float rolloffDistanceMin = 1f)
    {
        GameObject impactSfxInstance = new GameObject();
        impactSfxInstance.transform.position = position;

        AudioSource source = impactSfxInstance.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = spatialBlend;
        source.minDistance = rolloffDistanceMin;
        source.Play();

        source.outputAudioMixerGroup = GetAudioGroup(audioGroup);

        TimedSelfDestruct timedSelfDestruct = impactSfxInstance.AddComponent<TimedSelfDestruct>();
        timedSelfDestruct.SetLifeTime(clip.length);
    }

    public static AudioMixerGroup GetAudioGroup(AudioGroups group)
    {
        var groups = AudioManager.FindMatchingGroups(group.ToString());

        if (groups.Length > 0)
            return groups[0];

        Debug.LogWarning($"Аудиогруппа для {group.ToString()} не найдена");
        return null;
    }

    public static void SetMasterVolume(float value)
    {
        float valueInDb = ValueInDb(value);

        AudioManager.SetFloat("MasterVolume", valueInDb);
    }

    public static float GetMasterVolume()
    {
        AudioManager.GetFloat("MasterVolume", out var valueInDb);

        return Mathf.Pow(10f, valueInDb / 20.0f);
    }

    public static void SetVolume(string name, float value)
    {
        float valueInDb = ValueInDb(value);

        AudioManager.SetFloat(name, valueInDb);
    }

    public static float GetVolume(string name)
    {
        AudioManager.GetFloat(name, out var valueInDb);

        return Mathf.Pow(10f, valueInDb / 20.0f);
    }

    public static void SetVolume(AudioGroups group, float value) => SetVolume(group.ToString(), value);

    public static float GetVolume(AudioGroups group) => GetVolume(group.ToString());

    public static void LoadAllVolume()
    {
        foreach (var value in Enum.GetValues(typeof(AudioGroups)))
        {
            string name = value.ToString();
            float volumeValue = GetVolume(name);
            SetVolume(name, volumeValue);
        }
    }

    private static float ValueInDb(float value)
    {
        if (value <= 0)
            value = 0.001f;

        return Mathf.Log10(value) * 20;
    }
}
