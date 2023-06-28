using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Consumables
{
    public class GrenadeSpawned : MonoBehaviour
    {
        public Action OnTake;
        
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private float _takeRadius = 3f;
        [SerializeField] private GrenadeType _type;
        [SerializeField] private MeshRenderer _renderer;
        
        private float _rotationSpeed = 150f;
        
        private void Start()
        {
            _sphereCollider.radius = _takeRadius;
        }

        private void Update()
        {
            HandleRotation();
        }

        private void OnTriggerEnter(Collider collider)
        {
            TryTake(collider);
        }

        public void Init(GrenadeType type, Material material)
        {
            _type = type;
            _renderer.material = material;
        }

        private void HandleRotation()
        {
            transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
        }

        private void TryTake(Collider collider)
        {
            var playerAmmo = collider.GetComponent<PlayerAmmo>();
            if (playerAmmo == null)
                return;
            
            OnTake?.Invoke();
            playerAmmo.TakeGrenade(_type);
        }
    }
}

