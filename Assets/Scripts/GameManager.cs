using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static int EventTime = 30;
    private int m_TotalZoombieCount = 0;
    private int m_AliveZoombieCount = 0;
    
    public Action OnGameComplete;
    public Action<bool> OnZoombieEvent;
    private void Awake()
    {
        m_TotalZoombieCount = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None).Length;
        m_AliveZoombieCount = m_TotalZoombieCount;
    }

    private void Start()
    {
        UIManager.Instance.UpdateQuestText(m_AliveZoombieCount);
    }

    public void UpdateQuestProgress()
    {
        if(m_AliveZoombieCount <= 0) return;
        m_AliveZoombieCount--;
        int killedZoombie = m_TotalZoombieCount - m_AliveZoombieCount;


        if (m_AliveZoombieCount == 10)
        {
            StartCoroutine(StartZoombieEvent());
        }
        
        if (killedZoombie == 10)
        {
            UIManager.Instance.ShowAchievements();
        }
        
        UIManager.Instance.UpdateQuestText(m_AliveZoombieCount);

        if (m_AliveZoombieCount > 0) return;
        UIManager.Instance.QuestComplete();
        OnGameComplete?.Invoke();
    }

    private IEnumerator StartZoombieEvent()
    {
        OnZoombieEvent?.Invoke(true);
        UIManager.Instance.ShowQuest(false);
        UIManager.Instance.ShowEventTimer(true);
        yield return new WaitForSecondsRealtime(EventTime);
        UIManager.Instance.ShowEventTimer(false);
        UIManager.Instance.ShowQuest(true);
        OnZoombieEvent?.Invoke(false);
    }
}
