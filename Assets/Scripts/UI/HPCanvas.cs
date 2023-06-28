using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPCanvas : MonoBehaviour
{
    private const string DAMAGE_CAPTION_FORMAT = "-{0}";
    
    private readonly WaitForSeconds _damageCaptionDelay = new WaitForSeconds(0.5f);
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _damageCaption;
    
    public void Init(int maxHP)
    {
        _canvas.worldCamera = Camera.main;;
        _damageCaption.enabled = false;
        
        _slider.minValue = 0;
        _slider.maxValue = maxHP;
        _slider.value = maxHP;
    }

    public void UpdateView(int currentHP, int damage)
    {
        _slider.value = currentHP;
        ShowDamageCaption(damage);
    }

    private void ShowDamageCaption(int damage)
    {
        _damageCaption.enabled = true;
        _damageCaption.text = String.Format(DAMAGE_CAPTION_FORMAT, damage);
        
        StopAllCoroutines();
        StartCoroutine(HideDamageCaption());
    }

    private IEnumerator HideDamageCaption()
    {
        yield return _damageCaptionDelay;
        
        _damageCaption.enabled = false;
    }
}
