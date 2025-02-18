using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int m_CurrentHealth;

    public Action OnDeath;
    public Action<int> OnTakeDamage;
    
    private void Start()
    {
        m_CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(m_CurrentHealth <= 0) return;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - damage, 0, maxHealth);
        OnTakeDamage?.Invoke(m_CurrentHealth);
        if (m_CurrentHealth > 0) return;
        Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}