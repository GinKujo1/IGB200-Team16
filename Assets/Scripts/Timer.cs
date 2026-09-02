using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    //private float timeRemaining = 60f;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    [Tooltip("Adjusts Timer in minutes.")]
    [SerializeField] private float timerTime;
    private float timerConverted;  // i didn't want to have to do math so we are converting into seconds internally
    

    private void Start()
    {
        isTimerRunning = true;  // this starts the timer when the scene starts can be modified later
        timerConverted = timerTime * 60;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timerConverted > 0)
            {
                // Subtract the time passed since the last frame
                timerConverted -= Time.deltaTime;
                DisplayTime(timerConverted);
            }
            else
            {
                Debug.Log("Time has run out!");
                timerConverted = 0;
                isTimerRunning = false;
                DisplayTime(timerConverted);
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
