using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "ScriptableObjects/EnemiesData", order = 1)]
public class EnemiesData : ScriptableObject
{
    public Enemy SpawnPrefab => _spawnPrefab;
    [SerializeField] private Enemy _spawnPrefab;

    public int MaxHP => _maxHP;
    [SerializeField] private int _maxHP;
}
