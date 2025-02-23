using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int m_CurrentHealth;

    public Action OnDeath;
    public Action<int,int> OnTakeDamage;
    internal bool IsInvincible;

    [SerializeField]
    public float invincibilityDuration;
    private bool isCooldownActive;
    
    private void Start()
    {
        m_CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(m_CurrentHealth <= 0 || IsInvincible || isCooldownActive) return;
        if (!isCooldownActive && invincibilityDuration > 0)
        {
            StartCoroutine(InvincibilityCooldown());
        }
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - damage, 0, maxHealth);
        OnTakeDamage?.Invoke(m_CurrentHealth, maxHealth);
        if (m_CurrentHealth > 0) return;
        Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    private IEnumerator InvincibilityCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isCooldownActive = false;
    }
}