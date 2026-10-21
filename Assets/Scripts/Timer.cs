using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public GameObject ResultsMenu;
    public GameObject EquationPanel;
    public GameObject mathManager;

    //private float timeRemaining = 60f;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isTimerRunning = false;
    [Tooltip("Adjusts Timer in minutes.")]
    [SerializeField] private float timerTime;
    private float timerConverted;  // i didn't want to have to do math so we are converting into seconds internally
    

    private void Start()
    {
        
        //isTimerRunning = true;  // this starts the timer when the scene starts can be modified later
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
                EquationPanel.SetActive(false);
                ResultsMenu.SetActive(true);
                mathManager.GetComponent<Equation>().ShowFinaleResults();
                timerConverted = 0;
                isTimerRunning = false;
                DisplayTime(timerConverted);

                //OnGameOver(mathManager.GetComponent<Equation>().Score); // getting score and added it to scoreboard.
                //OnGameOver(100);
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnGameOver(float finalScore) // temporary position for this function // Option 1: Prompt for name via UI InputField
                                                                    // Option 2: Use a default name(set at start or something or account based)
    {
        if (ScoreBoardManager.Instance.QualifiesForLeaderboard(finalScore))
        {
           
            string playerName = "Player"; // Replace with UI input

            ScoreBoardManager.Instance.AddScore(playerName, finalScore);
        }
    }
    public void StartTimer() {
        isTimerRunning = true;
    }
    //potentially submit score button
    //TMP_InputField nameInput;
    //public void SubmitScore(int finalScore)
    //{
    //    string playerName = string.IsNullOrWhiteSpace(nameInput.text)
    //        ? "Some Student"
    //        : nameInput.text;
    //
    //    // Trim to avoid absurdly long names
    //    if (playerName.Length > 12) playerName = playerName.Substring(0, 12);
    //
    //    ScoreBoardManager.Instance.AddScore(playerName, finalScore);
    //}
}
