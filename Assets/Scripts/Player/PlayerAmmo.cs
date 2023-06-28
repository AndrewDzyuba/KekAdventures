using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{
    public class PlayerAmmo : MonoBehaviour
    {
        public Action OnGrenadesAmountChange;
        public Action OnGrenadeChoose;
        
        private Dictionary<GrenadeType, int> _grenadeAmmos = new Dictionary<GrenadeType, int>(3)
        {
            {GrenadeType.Green, 0},
            {GrenadeType.Blue, 0},
            {GrenadeType.Red, 0},
        };

        public Dictionary<GrenadeType, int> GrenadeAmmos => _grenadeAmmos;
        public GrenadeType ChoosedGreande => _choosedGrenade;

        private GrenadeType _choosedGrenade;
        private int _grenadeTypeCount = Enum.GetValues(typeof(GrenadeType)).Length;

        private void Awake()
        {
            OnGrenadesAmountChange?.Invoke();
        }

        public void TakeGrenade(GrenadeType type)
        {
            _grenadeAmmos[type]++;
            OnGrenadesAmountChange?.Invoke();
        }

        public bool HaveGrenadeOfSelectedType()
        {
            return GrenadeAmmos[ChoosedGreande] > 0;
        }

        public bool TrySpendGrenade()
        {
            if (GrenadeAmmos[ChoosedGreande] <= 0)
                return false;

            GrenadeAmmos[ChoosedGreande]--;
            OnGrenadesAmountChange?.Invoke();
            return true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(InputSettings.NEXT_GRENADE))
            {
                _choosedGrenade = (int)_choosedGrenade + 1 >= _grenadeTypeCount ? 0 : _choosedGrenade + 1;
                OnGrenadeChoose?.Invoke();
            }
            else if (Input.GetKeyDown(InputSettings.PREV_GRENADE))
            {
                _choosedGrenade = (int)_choosedGrenade - 1 < 0 ? (GrenadeType)_grenadeTypeCount - 1 : _choosedGrenade - 1;
                OnGrenadeChoose?.Invoke();
            }
        }
    }
}
