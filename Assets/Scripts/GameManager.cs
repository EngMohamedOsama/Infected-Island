using System;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    public static int EventTime = 15;
    private int m_TotalZoombieCount = 0;
    private int m_AliveZoombieCount = 0;
    
    public Action OnGameComplete;
    public Action<bool> OnZoombieEvent;

    private int m_EventTrigger;

    public Character Character;

    protected override void Awake()
    {
        base.Awake();
        Character = FindAnyObjectByType<Character>();
    }

    private void Start()
    {
        m_TotalZoombieCount = FindObjectsByType<EnemyAi>(FindObjectsSortMode.None).Length;
        m_AliveZoombieCount = m_TotalZoombieCount;
        UIManager.Instance.UpdateQuestText(m_AliveZoombieCount);

        m_EventTrigger = Random.Range(10, 30);
    }

    public void UpdateQuestProgress()
    {
        if(m_AliveZoombieCount <= 0) return;
        m_AliveZoombieCount--;
        int killedZoombie = m_TotalZoombieCount - m_AliveZoombieCount;


        if (m_AliveZoombieCount == m_EventTrigger)
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
