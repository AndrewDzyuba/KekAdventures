using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Consumables;

[CreateAssetMenu(fileName = "GrenadesData", menuName = "ScriptableObjects/GrenadesData", order = 1)]
public class GrenadesData : ScriptableObject
{
    public List<GrenadeData> GrenadesDatas => _grenadesDatas;
    [SerializeField] private List<GrenadeData> _grenadesDatas = new List<GrenadeData>();

    public GrenadeSpawned SpawnPrefab => _spawnPrefab;
    [SerializeField] private GrenadeSpawned _spawnPrefab;
    
    public GrenadeThrow ThrowPrefab => _throwPrefab;
    [SerializeField] private GrenadeThrow _throwPrefab;

    public GrenadeData GetGrenadeData(GrenadeType type)
    {
        return GrenadesDatas.Find(x => x.type == type);
    }

    [Serializable]
    public class GrenadeData
    {
        public GrenadeType type;
        public Material material;
        public Color explosionColor;
        public Sprite icon;
    }
}