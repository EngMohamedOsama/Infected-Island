using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject m_Achivement;
    
    [SerializeField]
    private GameObject m_EventTimer;
    
    [SerializeField]
    private TMP_Text m_questText;

    [SerializeField] private Image healthBar;
    [SerializeField] private Animator damagePanel;

    [SerializeField] private TMP_Text m_GameFinishedText;
    [SerializeField] private FixedJoystick m_MovementJoystick;
    [SerializeField] private FixedJoystick m_AimJoystick;
    
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
        m_GameFinishedText.transform.parent.gameObject.SetActive(true);
        ShowQuest(false);
    }

    public void GameFailed()
    {
        m_GameFinishedText.text = "MISSION FAILED!";
        m_GameFinishedText.color = Color.red;
        m_GameFinishedText.transform.parent.gameObject.SetActive(true);
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

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float healthPercent = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = healthPercent;
        damagePanel.SetTrigger("damage");
        damagePanel.GetComponent<AudioSource>().Play();
    }

    public Vector2 GetMovementJoystick()
    {
        return new Vector2(m_MovementJoystick.Horizontal, m_MovementJoystick.Vertical);
    }

    public Vector2 GetAimJoystick()
    {
        return new Vector2(m_AimJoystick.Horizontal, m_AimJoystick.Vertical);
    }
    
    public void OnReload()
    {
        GameManager.Instance.Character.OnTryPlayReload();
    }

    public void OnRun(bool state)
    {
        GameManager.Instance.Character.OnTryRun(state);
    }
    
    public void OnTryFire(bool state)
    {
        GameManager.Instance.Character.OnTryFire(state);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
