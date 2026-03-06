using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLibrary", menuName = "Scriptable Objects/MapLibrary")]
public class MapLibrary : ScriptableObject
{
    public List<MapSO> mapLibrary;
}
