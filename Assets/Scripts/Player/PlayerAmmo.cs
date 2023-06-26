using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{
    public class PlayerAmmo : MonoBehaviour
    {
        public Action OnGrenadesChange;
        
        private Dictionary<GrenadeType, int> _grenadeAmmos = new Dictionary<GrenadeType, int>(3)
        {
            {GrenadeType.Green, 0},
            {GrenadeType.Blue, 0},
            {GrenadeType.Red, 0},
        };

        public Dictionary<GrenadeType, int> GrenadeAmmos => _grenadeAmmos;

        private void Awake()
        {
            OnGrenadesChange?.Invoke();
        }

        public void TakeGrenade(GrenadeType type)
        {
            _grenadeAmmos[type]++;
            OnGrenadesChange?.Invoke();
        }        
        
        public void Throw(GrenadeType type)
        {
            _grenadeAmmos[type]--;
            OnGrenadesChange?.Invoke();
        }
    }
}
