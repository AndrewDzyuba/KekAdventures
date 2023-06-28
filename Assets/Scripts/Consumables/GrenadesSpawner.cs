using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Consumables
{
    public class GrenadesSpawner : MonoBehaviour
    {
        private readonly WaitForSeconds _spawnDelay = new WaitForSeconds(5f);
        
        [Inject] private GrenadesData _grenadesData;
        
        private GrenadeSpawned _grenadeSpawned;

        private void Start()
        {
            InstantiateOnStart();
        }

        private void InstantiateOnStart()
        {
            var prefab = _grenadesData.SpawnPrefab;
            _grenadeSpawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            InitGrenade();

            _grenadeSpawned.OnTake += OnTake;
        }

        private void OnTake()
        {
            _grenadeSpawned.gameObject.SetActive(false);
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return _spawnDelay;

            _grenadeSpawned.gameObject.SetActive(true);
            InitGrenade();
        }

        private void InitGrenade()
        {
            var randomType = (GrenadeType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(GrenadeType)).Length);
            var grenadeMaterial = _grenadesData.GetGrenadeData(randomType).material;
            _grenadeSpawned.Init(randomType, grenadeMaterial);
        }
    }
}