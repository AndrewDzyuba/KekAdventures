using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Consumables
{
    public class GrenadesSpawnerNew : SpawnerBase
    {
        [Inject] private GrenadesData _grenadesData;
        
        private GrenadeSpawned _grenadeSpawned;
        
        protected override void SetPrefab()
        {
            var prefab = _grenadesData.SpawnPrefab;
            _spawnedItem = Instantiate(prefab.gameObject, transform.position, Quaternion.identity, transform);
            _grenadeSpawned = _spawnedItem.GetComponent<GrenadeSpawned>();
            InitItemData();

            _grenadeSpawned.OnTake += OnItemRemoved;
        }
        
        protected override void InitItemData()
        {
            var randomType = (GrenadeType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(GrenadeType)).Length);
            var grenadeMaterial = _grenadesData.GetGrenadeData(randomType).material;
            _grenadeSpawned.Init(randomType, grenadeMaterial);
        }
    }
}