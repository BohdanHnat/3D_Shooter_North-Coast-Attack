using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthView : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthImage;
    private void Start()
    {
        healthComponent.OnDamageDealt += OnDamaged;
        healthComponent.OnHealing += OnHealed;

        healthText.text = $"{healthComponent.maxHealth}";
    }
    public void SetFill(float fillAmount)
    {
        healthImage.fillAmount = fillAmount;
    }
    private void OnDamaged(int current, int max)
    {
        SetFill((float)current / (float)max);

        if (current > 0) healthText.text = $"{current}";
        else healthText.text = "";
    }
    private void OnHealed(int current, int max)
    {
        SetFill((float)current / (float)max);

        if (healthComponent.maxHealth >= current) healthText.text = $"{current}";
        else healthText.text = $"{healthComponent.maxHealth}";
    }
}
