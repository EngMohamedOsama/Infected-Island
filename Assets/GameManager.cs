using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int m_ZoombieCount = 0;
    public Action OnGameComplete;
    private void Awake()
    {
        m_ZoombieCount = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None).Length;
    }

    private void Start()
    {
        UIManager.Instance.UpdateQuestText(m_ZoombieCount);
    }

    public void UpdateQuestProgress()
    {
        if(m_ZoombieCount <= 0) return;
        
        m_ZoombieCount--;
        UIManager.Instance.UpdateQuestText(m_ZoombieCount);

        if (m_ZoombieCount > 0) return;
        UIManager.Instance.QuestComplete();
        OnGameComplete?.Invoke();
    }

}
