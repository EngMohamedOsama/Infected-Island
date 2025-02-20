using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
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
        m_questText.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
