using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public TMP_Text feedbackText;

    public TMP_Text scoreText;
    public TMP_Text streakText;
    public TMP_Text multiplierText;
    public TMP_Text timerText;

    public GameObject resultsPanel;

    public TMP_Text finalScoreText;
    public TMP_Text questionsText;
    public TMP_Text correctText;
    public TMP_Text accuracyText;
    public TMP_Text bestStreakText;
    public TMP_Text incorrectQuestionsText;

    // 4-minute round
    public float roundTime = 240f;
    private float timeRemaining;
    private bool gameActive = true;

    private int score = 0;
    private int streak = 0;
    private int multiplier = 1;
    private int questionsAnswered = 0;
    private int correctAnswers = 0;
    private int bestStreak = 0;

    private int correctAnswer;
    private string incorrectQuestions = "";

    void Start()
    {
        timeRemaining = roundTime;

        feedbackText.text = "";

        GenerateQuestion();
        UpdateUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (!gameActive)
            return;

        // Count down the round timer
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }

        UpdateTimerUI();
    }

    public void SubmitAnswer()
    {
        // Don't allow answers after the timer has finished
        if (!gameActive)
            return;

        if (int.TryParse(answerInput.text, out int playerAnswer))
        {
            questionsAnswered++;

            if (playerAnswer == correctAnswer)
            {
                correctAnswers++;

                streak++;

                if (streak > bestStreak)
                    bestStreak = streak;

                int pointsEarned = 100 * multiplier;
                score += pointsEarned;

                if (streak >= 3)
                    multiplier = 2;

                if (streak >= 6)
                    multiplier = 3;

                feedbackText.text = "Correct! + " + pointsEarned;
            }
            else
            {
                // Record the incorrect question
                incorrectQuestions += questionText.text + "\n";
                incorrectQuestions += "Your answer: " + playerAnswer + "\n";
                incorrectQuestions += "Correct answer: " + correctAnswer + "\n\n";

                streak = 0;
                multiplier = 1;

                feedbackText.text = "Incorrect! Streak lost.";
            }

            answerInput.text = "";

            UpdateUI();
            GenerateQuestion();
        }
        else
        {
            feedbackText.text = "Please enter a number.";
        }
    }

    void GenerateQuestion()
    {
        int number1 = Random.Range(1, 50);
        int number2 = Random.Range(1, 50);

        correctAnswer = number1 + number2;

        questionText.text = number1 + " + " + number2 + " = ?";
    }

    void UpdateUI()
    {
        scoreText.text = "SCORE: " + score;
        streakText.text = "STREAK: " + streak;
        multiplierText.text = "×" + multiplier + " MULTIPLIER";
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowResults()
    {
        finalScoreText.text = "FINAL SCORE: " + score;

        questionsText.text = "QUESTIONS ANSWERED: " + questionsAnswered;

        correctText.text = "CORRECT ANSWERS: " + correctAnswers + " / " + questionsAnswered;

        float accuracy = 0f;

        if (questionsAnswered > 0)
            accuracy = ((float)correctAnswers / questionsAnswered) * 100f;

        accuracyText.text = "ACCURACY: " + Mathf.RoundToInt(accuracy) + "%";

        bestStreakText.text = "BEST STREAK: " + bestStreak;

        if (incorrectQuestions == "")
        {
            incorrectQuestionsText.text = "INCORRECT QUESTIONS\n\nNo incorrect answers!";
        }
        else
        {
            incorrectQuestionsText.text = "INCORRECT QUESTIONS\n\n" + incorrectQuestions;
        }

        resultsPanel.SetActive(true);
    }

    void EndGame()
    {
        gameActive = false;

        feedbackText.text = "TIME'S UP!";

        answerInput.interactable = false;

        ShowResults();

        Debug.Log("Game Over! Final Score: " + score);
    }
}