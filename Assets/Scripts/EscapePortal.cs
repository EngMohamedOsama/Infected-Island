using System;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class EscapePortal : MonoBehaviour
{
    private Collider m_collider;
    private void Awake()
    {
        GameManager.Instance.OnGameComplete += GameComplete;
        m_collider = GetComponent<Collider>();
    }

    private void GameComplete()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        m_collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.GetComponent<Movement>().enabled = false;
        other.GetComponent<Character>().UnlockCursor();
        UIManager.Instance.GameFinished();
        m_collider.enabled = false;
    }
}
