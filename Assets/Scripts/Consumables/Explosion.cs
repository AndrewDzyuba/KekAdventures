using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private WaitForSeconds _despawnDelay = new WaitForSeconds(2f);
    
    public void Init(Color color)
    {
        var main = _particleSystem.main;
        main.startColor = color;

        StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return _despawnDelay;
        
        Pooling.Despawn(gameObject);
    }
}
