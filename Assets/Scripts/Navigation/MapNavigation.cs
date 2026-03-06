using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapNavigation : MonoBehaviour
{
    [SerializeField] private MapLibrary mapLibrary;
    [SerializeField] private Transform player;
    [SerializeField] private Transform mapParent;
    [SerializeField] private int startingMapID = 0;

    private Dictionary<int, MapData> mapDictionary = new Dictionary<int, MapData>();
    public static MapNavigation Instance;
    public event Action<PlayerAnimationState> OnEnterMap;
    public GameObject currentMap;

    public UnityEvent OnNavigate;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeMapLibrary();
        SpawnStartingMap(startingMapID);
    }

    private void SpawnStartingMap(int mapID)
    {
        foreach (Transform child in mapParent)
            Destroy(child.gameObject);

        currentMap = Instantiate(mapDictionary[mapID].prefab, mapParent);
        StartCoroutine(InitMap(mapID));
    }

    public void InitializeMapLibrary()
    {
        foreach (MapSO map in mapLibrary.mapLibrary)
        {
            mapDictionary.Add(map.mapID, new MapData(map));
        }
    }

    public void GoToMap(int mapID, int entryPointID)
    {
        GameStateManager.Instance.SaveCurrentMapState();

        EnemySpawner oldSpawner = currentMap.GetComponentInChildren<EnemySpawner>();
        if (oldSpawner != null)
        {
            oldSpawner.ClearEnemies();
        }
        Destroy(currentMap);

        currentMap = Instantiate(mapDictionary[mapID].prefab, mapParent);

        Grid g = currentMap.GetComponent<Grid>();
        Vector3 newPosition = g.GetCellCenterWorld(mapDictionary[mapID].entryPoints[entryPointID].cell);
        player.position = newPosition;

        StartCoroutine(InitMap(mapID));
        OnNavigate?.Invoke();
    }

    private IEnumerator InitMap(int mapID)
    {
        yield return new WaitForEndOfFrame();
        GameStateManager.Instance.InitializeMap(mapID);
    }
}

public class MapData
{
    public GameObject prefab;
    public int mapID;
    public string mapName;
    public Dictionary<int, MapEntryPoint> entryPoints = new Dictionary<int, MapEntryPoint>();

    public MapData(MapSO config)
    {
        this.prefab = config.prefab;
        this.mapID = config.mapID;
        this.mapName = config.mapName;

        foreach (MapEntryPoint entryPoint in config.entryPoints)
        {
            entryPoints.Add(entryPoint.entryPointID, entryPoint);
        }
    }
}
