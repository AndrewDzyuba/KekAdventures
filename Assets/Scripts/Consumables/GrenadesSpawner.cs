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
        [Inject] private GrenadesData _grenadesData;

        private WaitForSeconds _spawnDelay = new WaitForSeconds(5f);
        private Grenade _grenade;

        private void Start()
        {
            InstantiateOnStart();
        }

        private void InstantiateOnStart()
        {
            var prefab = _grenadesData.SpawnPrefab;
            _grenade = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            InitGrenade();

            _grenade.OnTake += OnTake;
        }

        private void OnTake()
        {
            _grenade.gameObject.SetActive(false);
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return _spawnDelay;

            _grenade.gameObject.SetActive(true);
            InitGrenade();
        }

        private void InitGrenade()
        {
            var randomType = (GrenadeType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(GrenadeType)).Length);
            var grenadeMaterial = _grenadesData.GetGrenadeData(randomType).material;
            _grenade.Init(randomType, grenadeMaterial);
        }
        
    }
}