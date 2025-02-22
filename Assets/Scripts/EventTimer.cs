using System;
using UnityEngine;
using TMPro;

public class EventTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to TextMeshProUGUI component
    private float timeRemaining = 60f; // Set timer for 60 seconds
    private bool isRunning = false;

    private void OnEnable()
    {
        timeRemaining = GameManager.EventTime;
        isRunning = true;
    }

    private void OnDisable()
    {
        isRunning = false;
    }

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer TextMeshProUGUI component is not assigned!");
            return;
        }

        isRunning = true; // Start the timer immediately
    }

    void Update()
    {
        if (isRunning)
        {
            // Decrease time remaining
            timeRemaining -= Time.deltaTime;

            // Format time remaining in minutes:seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Update UI
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Stop timer when time is up
            if (timeRemaining <= 0)
            {
                isRunning = false;
                timerText.text = "00:00"; // Display 00:00 when time runs out
            }
        }
    }
}