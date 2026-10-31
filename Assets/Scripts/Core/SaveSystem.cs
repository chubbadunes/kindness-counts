using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath
    {
        get { return Path.Combine(Application.persistentDataPath, "kindness_save.json"); }
    }

    public static PlayerProfile Load()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                var profile = JsonUtility.FromJson<PlayerProfile>(File.ReadAllText(SavePath));
                if (profile != null && !string.IsNullOrEmpty(profile.PlayerId))
                    return profile;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Save load failed: " + e.Message);
        }
        return NewProfile();
    }

    public static PlayerProfile NewProfile()
    {
        return new PlayerProfile
        {
            PlayerId = Guid.NewGuid().ToString("N"),
            CreatedUtc = DateTime.UtcNow.Ticks
        };
    }

    public static void Save(PlayerProfile profile)
    {
        try
        {
            File.WriteAllText(SavePath, JsonUtility.ToJson(profile, true));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Save failed: " + e.Message);
        }
    }
}
