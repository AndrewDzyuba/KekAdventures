using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Action OnDie;
    
    [SerializeField] private HPCanvas _hpCanvas;

    private int _currentHP = 100;

    public void Init(int maxHP)
    {
        _currentHP = maxHP;
        _hpCanvas.Init(maxHP);
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
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }
}
