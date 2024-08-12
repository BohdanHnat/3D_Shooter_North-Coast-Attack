using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour, IDamageble
{
    public int maxHealth;
    public int currentHealth { get; private set; }

    public event Action<int, int> OnDamageDealt;
    public event Action<int, int> OnHealing;
    
    public event Action OnDie;
    public event Action OnEnemyDie;
    private void Start()
    {
        currentHealth = maxHealth;
        OnDie += OnDestroyed;
    }
    public void RegisterDamage(int damage)
    {
        currentHealth -= damage;

        OnDamageDealt?.Invoke(currentHealth, maxHealth);
        
        if (currentHealth <= 0) OnDie?.Invoke();
    }
    public void OnHealed(int amount)
    {
        currentHealth += amount;
        
        Context.Instance.AudioSystem.PlaySFX(new AudioData("heal", volume: 1f));
        
        OnHealing.Invoke(currentHealth, maxHealth);
    }
    private void OnDestroyed()
    {
        if (tag != "Player") OnEnemyDie?.Invoke();
    }
}
