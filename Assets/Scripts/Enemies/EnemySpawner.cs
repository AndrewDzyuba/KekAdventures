using System;
using System.Collections;
using System.Collections.Generic;
using Consumables;
using UnityEngine;
using VContainer;

public class EnemySpawner : SpawnerBase
{
    [Inject] private EnemiesData _enemiesData;
        
    private Enemy _enemySpawned;
        
    protected override void SetPrefab()
    {
        var prefab = _enemiesData.SpawnPrefab;
        float enemyRotation = 180f;
        _spawnedItem = Instantiate(prefab.gameObject, transform.position, Quaternion.Euler(0f, enemyRotation, 0f), transform);
        _enemySpawned = _spawnedItem.GetComponent<Enemy>();
        InitItemData();

        _enemySpawned.OnDie += OnItemRemoved;
    }
        
    protected override void InitItemData()
    {
        var maxHP = _enemiesData.MaxHP;
        _enemySpawned.Init(maxHP);
    }
}
