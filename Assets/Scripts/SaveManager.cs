using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private const string SaveFileName = "savegame.json";
    public static string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(SaveFilePath, json);
        Debug.Log($"[SaveManager] Game saved to {SaveFilePath}");
    }

    public static SaveData Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.Log("[SaveManager] No save file found.");
            return null;
        }

        string json = File.ReadAllText(SaveFilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveManager] Game loaded from {SaveFilePath}");
        return data;
    }

    public static void DeleteSave()
    {
        if (File.Exists(SaveFilePath))
            File.Delete(SaveFilePath);
    }
}

[Serializable]
public class SaveData
{
    public int playerHealth;
    public int playerGold;
    public List<MapSaveData> mapStates = new List<MapSaveData>();
}

[Serializable]
public class MapSaveData
{
    public int mapID;
    public List<EnemySaveData> enemyStates = new List<EnemySaveData>();
    public List<int> collectedTreasureIDs = new List<int>();
}

[Serializable]
public class EnemySaveData
{
    public int enemyID;
    public int currentHP;
}
