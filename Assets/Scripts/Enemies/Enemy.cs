using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private HPCanvas _hpCanvas;

    private int _currentHP = 100;
    
    private void Start()
    {
        _currentHP = _maxHP;
        _hpCanvas.Init(_maxHP);
    }

    public void ApplyDamage(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            Die();
            return;
        }
        
        _hpCanvas.UpdateView(_currentHP, damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
