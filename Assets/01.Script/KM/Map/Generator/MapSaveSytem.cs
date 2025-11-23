using System.IO;
using UnityEngine;

public static class MapSaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "map_save.json");

    public static void Save(MapSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static MapSaveData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        string json = File.ReadAllText(SavePath);
        var data = JsonUtility.FromJson<MapSaveData>(json);
        return data;
    }

    public static void Delete()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
