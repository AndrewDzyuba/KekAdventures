using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Explosion _explosionPrefab;

    private Color _explosionColor;
    
    public void Init(Vector3 velocity, Material material, Color explosionColor)
    {
        _rigidbody.velocity = velocity;
        _renderer.material = material;
        _explosionColor = explosionColor;
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    private void Explode()
    {
        var explosion = Pooling.Spawn(_explosionPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Explosion>();
        explosion.Init(_explosionColor);
        
        Pooling.Despawn(gameObject);
    }
}
