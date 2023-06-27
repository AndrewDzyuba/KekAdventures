using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class GrenadeView : MonoBehaviour
    {
        [SerializeField] private GrenadeType _type;
        [SerializeField] private  TextMeshProUGUI _amount;
        [SerializeField] private  Image _icon;
        [SerializeField] private  Image _glow;

        [Inject] private PlayerAmmo _playerAmmo;
        [Inject] private GrenadesData _grenadesData;
        
        private void Start()
        {
            SetIcon();
            UpdateAmount();
            UpdateChoosedGrenade();
            
            _playerAmmo.OnGrenadesAmountChange += UpdateAmount;
            _playerAmmo.OnGrenadeChoose += UpdateChoosedGrenade;
        }

        private void OnDestroy()
        {
            _playerAmmo.OnGrenadesAmountChange -= UpdateAmount;
            _playerAmmo.OnGrenadeChoose -= UpdateChoosedGrenade;
        }

        private void SetIcon()
        {
            var data = _grenadesData.GetGrenadeData(_type);
            _icon.sprite = data.icon;
        }

        private void UpdateAmount()
        {
            var text = _playerAmmo.GrenadeAmmos[_type].ToString();
            _amount.text = text;
        }

        private void UpdateChoosedGrenade()
        {
            _glow.enabled = _playerAmmo.ChoosedGreande == _type;
        }
    }
}
