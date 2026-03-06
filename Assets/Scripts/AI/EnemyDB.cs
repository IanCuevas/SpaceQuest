using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Enemy Database")]
public class EnemyDB : ScriptableObject
{
    public List<EnemySpawnData> enemies;

    private Dictionary<int, EnemySO> lookup;

    public void BuildLookup()
    {
        lookup = new Dictionary<int, EnemySO>();
        foreach (EnemySpawnData enemy in enemies)
        {
            lookup.Add(enemy.enemyID, enemy.enemySO);
        }
    }

    public EnemySO Get(int id)
    {

        if (lookup == null) BuildLookup();
        return lookup[id];
    }
}

[Serializable]
public class EnemySpawnData
{
    public EnemySO enemySO;
    public int enemyID;
}
