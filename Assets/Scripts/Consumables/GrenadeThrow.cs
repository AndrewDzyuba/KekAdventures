using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private float _explosionRadius = 30f;
    [SerializeField] private int _damage = 45;
    
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

        TryHit();
        
        Pooling.Despawn(gameObject);
    }

    private void TryHit()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<Enemy>();
            if (enemy == null)
                continue;
            
            enemy.ApplyDamage(_damage);
        }
    }
}
