using UnityEngine;
using System;

public class SaveLoadHandler<T>
{
    private const string _playerPrefsHeader = "SaveLoadHandler";

    public T Load(string key)
    {
        var fullKey = PlayerPrefs.GetString(GenerateKey(key));

        if (PlayerPrefs.HasKey(fullKey) == false)
            throw new ArgumentOutOfRangeException();

       return JsonUtility.FromJson<T>(PlayerPrefs.GetString(fullKey));
    }

    public bool TryLoad(string key, out T value)
    {
        value = default(T);

        try
        {
           value = Load(key);

           return true;
        }
        catch
        {
            return false;
        }
    }

    public void Save(T value, string key)
    {
        var fullKey = PlayerPrefs.GetString(GenerateKey(key));

        PlayerPrefs.SetString(fullKey, JsonUtility.ToJson(value));
    }

    public void Clear(string key)
    {
        var fullKey = PlayerPrefs.GetString(GenerateKey(key));

        PlayerPrefs.DeleteKey(fullKey);
    }

    private string GenerateKey(string baseKey)
    {
        return _playerPrefsHeader + baseKey;
    }
}
