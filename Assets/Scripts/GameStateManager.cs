using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState gameState;
    public Transform mapParent;

    private EnemySpawner spawner;
    private MapState currentMapState;

    private readonly Dictionary<int, MapState> mapStateDictionary = new Dictionary<int, MapState>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveData save = SaveManager.Load();

        if (save != null)
        {
            if (PlayerStats.Instance != null)
                PlayerStats.Instance.ReadFromSaveData(save);

            BuildMapStatesFromSave(save);
        }
        else
        {
            BuildMapStatesFromDefaults();
        }

        InitializeMap(0);
    }

    private void BuildMapStatesFromDefaults()
    {
        mapStateDictionary.Clear();
        foreach (MapState mapState in gameState.mapStates)
        {
            mapState.InitializeDictionary();
            mapStateDictionary[mapState.mapID] = mapState;
        }
    }

    private void BuildMapStatesFromSave(SaveData save)
    {
        BuildMapStatesFromDefaults();

        foreach (MapSaveData savedMap in save.mapStates)
        {
            if (!mapStateDictionary.TryGetValue(savedMap.mapID, out MapState state))
                continue;

            foreach (EnemySaveData savedEnemy in savedMap.enemyStates)
            {
                if (state.enemyDictionary.TryGetValue(savedEnemy.enemyID, out EnemyState enemyState))
                    enemyState.currentHP = savedEnemy.currentHP;
            }

            state.collectedTreasureIDs = new HashSet<int>(savedMap.collectedTreasureIDs);
        }
    }

    public void InitializeMap(int mapID)
    {
        if (!mapStateDictionary.TryGetValue(mapID, out MapState state))
        {
            Debug.LogWarning($"[GameStateManager] No MapState found for mapID {mapID}");
            return;
        }

        currentMapState = state;
        BeginEnemySpawn(currentMapState);
        HideCollectedTreasures(currentMapState);
    }

    private void BeginEnemySpawn(MapState map)
    {
        spawner = mapParent.GetComponentInChildren<EnemySpawner>();
        if (spawner == null) return;

        foreach (EnemyState enemy in map.enemyStates)
        {
            if (enemy.currentHP > 0)
                spawner.Spawn(enemy.enemyID, enemy.currentHP);
        }
    }

    private void HideCollectedTreasures(MapState map)
    {
        Treasure[] treasures = mapParent.GetComponentsInChildren<Treasure>();
        foreach (Treasure t in treasures)
        {
            if (map.collectedTreasureIDs.Contains(t.treasureID))
                t.gameObject.SetActive(false);
        }
    }

    public void SaveCurrentMapState()
    {
        if (mapParent != null)
            spawner = mapParent.GetComponentInChildren<EnemySpawner>();

        if (spawner == null || currentMapState == null) return;

        foreach (Enemy enemy in spawner.activeEnemies)
        {
            if (enemy == null) continue;

            if (currentMapState.enemyDictionary.TryGetValue(enemy.enemyID, out EnemyState state))
                state.currentHP = enemy.HP;
        }
    }

    public void RegisterTreasureCollected(int mapID, int treasureID)
    {
        if (mapStateDictionary.TryGetValue(mapID, out MapState state))
            state.collectedTreasureIDs.Add(treasureID);
    }

    public void ResetAllEnemies()
    {
        foreach (MapState m in mapStateDictionary.Values)
        {
            foreach (EnemyState e in m.enemyStates)
                e.currentHP = e.maxHP;
        }
    }

    [ContextMenu("Save Game")]
    public void SaveToDisk()
    {
        SaveCurrentMapState();

        SaveData data = new SaveData();

        if (PlayerStats.Instance != null)
            PlayerStats.Instance.WriteToSaveData(data);

        foreach (MapState map in mapStateDictionary.Values)
        {
            MapSaveData mapSave = new MapSaveData { mapID = map.mapID };

            foreach (EnemyState e in map.enemyStates)
                mapSave.enemyStates.Add(new EnemySaveData { enemyID = e.enemyID, currentHP = e.currentHP });

            mapSave.collectedTreasureIDs.AddRange(map.collectedTreasureIDs);
            data.mapStates.Add(mapSave);
        }

        SaveManager.Save(data);
    }
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;

    [NonSerialized] public Dictionary<int, EnemyState> enemyDictionary;
    [NonSerialized] public HashSet<int> collectedTreasureIDs = new HashSet<int>();

    public void InitializeDictionary()
    {
        enemyDictionary = new Dictionary<int, EnemyState>();
        foreach (EnemyState enemy in enemyStates)
            enemyDictionary[enemy.enemyID] = enemy;
    }
}

[Serializable]
public class EnemyState
{
    public int enemyID;
    public int currentHP;
    public int maxHP;
}

[Serializable]
public class GameState
{
    public List<MapState> mapStates;
}
