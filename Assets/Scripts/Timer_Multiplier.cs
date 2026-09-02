using TMPro;
using UnityEngine;

public class Timer_Multiplier : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    [Tooltip("Adjusts Timer seconds")]
    [SerializeField] private float multiplierTimer = 11f;
    private float timeRemaining;


    private void Start()
    {
        isTimerRunning = true;  // this starts the timer when the scene starts can be modified later
        timeRemaining = multiplierTimer;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                // Subtract the time passed since the last frame
                
                
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                isTimerRunning = false;
                DisplayTime(timeRemaining);
                //nextQuestion();
            }
        }
    }
    public void nextQuestion()
    {
        Debug.Log("Next question");
        timeRemaining = multiplierTimer;
        isTimerRunning = true;
    }

    private void DisplayTime(float timeToDisplay)
    {

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}", seconds);
    }
}
