using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject m_Achivement;
    
    [SerializeField]
    private GameObject m_EventTimer;
    
    [SerializeField]
    private TMP_Text m_questText;

    [SerializeField] private GameObject m_GameFinished;
    public void UpdateQuestText(int totalQuests)
    {
        m_questText.text = $"YOUR QUEST:-\nKILL ALL ZOOMBIES: {totalQuests}";
    }

    public void QuestComplete()
    {
        m_questText.text = "QUEST COMPLETE\nFIND THE MAGIC PORTAL\nTO ESCAPE ZOOMBIES ISLAND";
    }

    public void GameFinished()
    {
        m_GameFinished.SetActive(true);
        ShowQuest(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowAchievements()
    {
        m_Achivement.SetActive(true);
    }
    
    public void ShowEventTimer(bool state)
    {
        m_EventTimer.SetActive(state);
    }    
    
    public void ShowQuest(bool state)
    {
        m_questText.gameObject.transform.parent.gameObject.SetActive(state);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
