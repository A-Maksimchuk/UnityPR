using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textView;
    [SerializeField] private Image imageScale;

    public void SetValue(float currentValue, float maxValue)
    {
        textView.text = $"{currentValue}/{maxValue}";
        imageScale.fillAmount = currentValue / maxValue;
    }
}
