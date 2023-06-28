using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;

    protected GameObject _spawnedItem;
    private WaitForSeconds _spawnYield;

    
    private void Start()
    {
        _spawnYield = new WaitForSeconds(_spawnDelay);
        
        SetPrefab();
    }

    protected virtual void SetPrefab()
    {
    }

    protected virtual void InitItemData()
    {
    }
    
    protected void SetSpawnedItemActive(bool active)
    {
        _spawnedItem.SetActive(active);
    }

    protected void OnItemRemoved()
    {
        StartCoroutine(SpawnCoroutine());
    }

    protected IEnumerator SpawnCoroutine()
    {
        yield return _spawnYield;

        SetSpawnedItemActive(true);
        InitItemData();
    }
}
