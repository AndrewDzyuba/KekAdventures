using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private readonly WaitForSeconds _despawnDelay = new WaitForSeconds(2f);
    
    [SerializeField] private ParticleSystem _particleSystem;
    
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
