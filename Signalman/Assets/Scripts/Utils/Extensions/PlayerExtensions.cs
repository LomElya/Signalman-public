using System;
using UnityEngine;

public static class PlayerExtensions
{
    public static void Save(string name, float value) => PlayerPrefs.SetFloat(name, value);
    public static void Save(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        Debug.Log(name + " " + value);
    }
    public static void Save(string name, string value) => PlayerPrefs.SetString(name, value);
    public static void Save(string name, bool value) => PlayerPrefs.SetString(name, value.ToString());

    public static void SaveLevelSeconds(string name, float seconds)
    {
        float value;
        LoadLevelSeconds(name, out value);

        if (PlayerPrefs.HasKey(name))
        {
            if (seconds < value)
                PlayerPrefs.SetFloat(name, seconds);
        }
        else
            PlayerPrefs.SetFloat(name, seconds);
    }

    public static void LoadLevelSeconds(string name, out float seconds)
    {
        seconds = 0;

        if (PlayerPrefs.HasKey(name))
            seconds = PlayerPrefs.GetFloat(name);
    }

    public static void Load(string name, out string value) => value = PlayerPrefs.GetString(name);
    public static void Load(string name, out float value)
    {
        if (PlayerPrefs.HasKey(name))
            value = PlayerPrefs.GetFloat(name);
        else
            value = 0f;
    }

    public static void Load(string name, out int value)
    {
        if (PlayerPrefs.HasKey(name))
            value = PlayerPrefs.GetInt(name);
        else
            value = 0;
    }

    public static bool Load(string name)
    {
        string result = PlayerPrefs.GetString(name);

        if (result == "true" || result == "True")
            return true;
        else
            return false;
    }

    public static void ResetSave() => PlayerPrefs.DeleteAll();
}
