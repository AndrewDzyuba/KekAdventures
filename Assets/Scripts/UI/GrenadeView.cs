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
        [Inject] private PlayerAmmo _playerAmmo;
        [Inject] private GrenadesData _grenadesData;
        
        [SerializeField] private GrenadeType _type;
        [SerializeField] private  TextMeshProUGUI _amount;
        [SerializeField] private  Image _icon;

        private void Start()
        {
            SetIcon();
            UpdateAmount();
            
            _playerAmmo.OnGrenadesChange += UpdateAmount;
        }

        private void OnDestroy()
        {
            _playerAmmo.OnGrenadesChange -= UpdateAmount;
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
    }
}
