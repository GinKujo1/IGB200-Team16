using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer_Multiplier : MonoBehaviour
{

    [SerializeField] private Equation mathManager;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    [Tooltip("Adjusts Timer seconds")]
    [SerializeField] private float multiplierTimer = 11f;
    [SerializeField] private Slider timerSlider;
    public float timeRemaining;


    private void Start()
    {
        isTimerRunning = true;  // this starts the timer when the scene starts can be modified later
        timerSlider.maxValue = multiplierTimer;
        timerSlider.value = timeRemaining;
        timeRemaining = multiplierTimer;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 1)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                isTimerRunning = false;
                DisplayTime(timeRemaining);
                mathManager.resetStreak();
                mathManager.displayStreak();
                mathManager.resetMulti();
                mathManager.displayMulti();
                //nextQuestion();
            }
        }
    }
    public void nextQuestion()
    {
        //Debug.Log("Next question");
        timeRemaining = multiplierTimer;
        timerSlider.value = multiplierTimer;
        isTimerRunning = true;
    }

    private void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}", seconds);
        timerSlider.value = seconds;
    }
}
