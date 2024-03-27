using UnityEngine;
using UnityEngine.Audio;

public class AudioManager
{
    private AudioMixer _audioMixer;

    public AudioManager(AudioMixer audioMixer) => _audioMixer = audioMixer;

    public AudioMixerGroup[] FindMatchingGroups(string subPath)
    {

        AudioMixerGroup[] results = _audioMixer.FindMatchingGroups(subPath);

        if (results != null && results.Length != 0)
            return results;


        return null;
    }

    public void SetFloat(string name, float value)
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat(name, value);
            PlayerPrefs.SetFloat(name, value);
            PlayerPrefs.Save();
        }

    }

    public void GetFloat(string name, out float value)
    {
        value = 0f;
        if (_audioMixer != null)
        {

            value = PlayerPrefs.GetFloat(name);
            /* if (PlayerPrefs.HasKey(name))
            {
                Debug.Log($"У {name} колв0о {value}");
                value = PlayerPrefs.GetFloat(name);
            }
            else
                value = 100f; */

            //  _audioMixer.GetFloat(name, out value);
        }

    }
}
