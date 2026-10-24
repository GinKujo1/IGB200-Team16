using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.TimeZoneInfo;




public class Equation : MonoBehaviour
{
    [Header("Gameplay Variables")]
    [SerializeField] private TMP_Text FeedbackText;
    [SerializeField] public TMP_InputField EnterAnswer;
    public TMP_Text scoreText;
    [SerializeField] private TMP_Text equationText;
    [SerializeField] private TMP_Text multiText;
    [SerializeField] private TMP_Text StreakText;
    [SerializeField] private TMP_Text FloorText;

    [Header("StreakEffect Variables")]
    [SerializeField] private CameraBehavior cameraObj;
    [SerializeField] private bool cameraShake;
    [SerializeField] private float cameraShakeDuration = 0.6f;
    [SerializeField] private float cameraShakeMagnitude = 0.4f;
    [SerializeField] private GameObject StreakTimerFillObj;
    [SerializeField] private GameObject StreakBackground;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] [Range(0, 1)] private float flashRatio = 0.5f;
    [SerializeField] private Color StreakInactiveColor;
    [SerializeField] private Color StreakActiveColor;
    [SerializeField] private Color StreakAddedColor;
    [SerializeField] private Color StreakResetColor;

    [Header("Gameplay Variables")]
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject timerMulti;
    public int _minValue = 1;
    public int _maxValue = 100;
   

    public enum Operations { Add, Subtract, Multiply, Division};
    public Operations operation = Operations.Multiply;

    public bool RandomOperations = false;
    // private vars
    public float Score { get; private set; }
    private Camera main;
    private int Number1;
    private int Number2;
    private int Answer;
    private int UserAnswer;
   
    //////////////////////////////////////////////////
    private float multi = 1;
    private float streakCurrent = 1;
    private float streakHighest = 0;
    private float questionsCorrect = 0;
    private float totalQuestions = 0;
    private int buildingIndex = 0;
    //private string incorrectQuestions = "";

    [Header("WinCard Text Variables")]
    public TMP_Text finalScoreText;
    public TMP_Text finalQuestionsText;
    public TMP_Text finalCorrectText;
    public TMP_Text finalAccuracyText;
    public TMP_Text finalBestStreakText;

    //[Header("NOT IMPLEMENTED")]
    //public TMP_Text finalIncorrectQuestionsText; 


    private int RandomNumber()
    {
        return Random.Range(_minValue, _maxValue + 1);
    }

    private Operations GetRandomOperations()
    {
        Operations[] ops = { Operations.Add, Operations.Subtract, Operations.Multiply, Operations.Division};
        return ops[Random.Range(0, ops.Length)];

    }
    private string GetOperationSymbol(Operations op)
    {
        switch (op)
        {
            case Operations.Add: return "+";
            case Operations.Subtract: return "-";
            case Operations.Multiply: return "×";
            case Operations.Division: return "÷";
            default: return "";
        }
    }


    private void GenerateEquation()
    {
        if (RandomOperations)
        {
            operation = GetRandomOperations();
        }
        switch (operation)
        {
            case Operations.Add:
                GenerateAddition();
                break;

            case Operations.Subtract:
                GenerateSubtraction();
                break;

            case Operations.Multiply:
                GenerateMultiplication();
                break;

            case Operations.Division:
            GenerateDivision();
               break;
        }

        equationText.text = $"{Number1} {GetOperationSymbol(operation)} {Number2} =";
    }

    private void GenerateAddition()
    {
        Mathf.Min(_minValue = 1);
        Mathf.Max(_maxValue = 100);

        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 + Number2;
    }
    private void GenerateSubtraction()
    {
        Mathf.Min(_minValue = 1);
        Mathf.Max(_maxValue = 50);

        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 - Number2;
    }
    private void GenerateMultiplication()
    {
        Mathf.Min(_minValue = 1);
        Mathf.Max(_maxValue = 12);

        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 * Number2;
    }
    private void GenerateDivision()
    {
        Mathf.Min(_minValue = 1);
        Mathf.Max(_maxValue = 12); 

        Number2 = RandomNumber();        
        Answer = RandomNumber();       
        Number1 = Number2 * Answer;                                  
    }
    
    public void CheckAnswer()
    {
        if (!int.TryParse(EnterAnswer.text, out UserAnswer))
        {
            FeedbackText.text = "Please enter a valid number";
            EnterAnswer.text = null;
            return;
        }

        if (UserAnswer == Answer)
        {
            totalQuestions++;
            FeedbackText.text = "CORRECT";
            questionsCorrect++;
            SoundManager.Instance.PlaySoundEffect(SFXType.SFX_CorrectAnswer);

            if(timerMulti.GetComponent<Timer_Multiplier>().timeRemaining > 0 || streakCurrent == 0)
            {
                StreakUp(1);
                displayStreak();
                StopAllCoroutines();
                StartCoroutine(PlayStreakFlashVFX(StreakAddedColor, StreakActiveColor));
            }
            switch(streakCurrent)
            {
                case 1:
                    SetMulti(1.0f);
                    buildingIndex = 0;
                    break;
                case 3:
                    SetMulti(1.2f);
                    buildingIndex = 0;
                    break;
                case 5:
                    SetMulti(1.5f);
                    buildingIndex = 1;
                    break;
                case 10:
                    SetMulti(2.0f);
                    buildingIndex = 2;
                    break;
                case 15:
                    SetMulti(3.0f);
                    buildingIndex = 3;
                    break;

            }
            displayMulti();
            
            main.GetComponent<CameraBehavior>().MoveCameraUp();
            //spawner.GetComponent<Spawner>().SpawnBuilding();
            spawner.GetComponent<Spawner>().SpawnBuildingFromlist(buildingIndex);
            timerMulti.GetComponent<Timer_Multiplier>().nextQuestion();
            ScoreUp(100, multi);
            displayTotalFloor();
            displayScore();
            NewEquation();
        }
        else
        {
            totalQuestions++;
            FeedbackText.text = $"INCORRECT, the answer was {Answer}.";
            SoundManager.Instance.PlaySoundEffect(SFXType.SFX_WrongAnswer);
            resetMulti();
            resetStreak();  ///reset streak
            if (cameraShake) { 
                cameraObj.StartCameraShake(cameraShakeDuration, cameraShakeMagnitude);
            }
            StopAllCoroutines();
            StartCoroutine(PlayStreakFlashVFX(StreakResetColor, StreakInactiveColor));
            buildingIndex = 0;
            displayStreak();
            displayMulti();
            timerMulti.GetComponent<Timer_Multiplier>().nextQuestion();
            NewEquation();
        }
    }



    void Update()
        {
            //if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            //{
            //    //NewEquation();
            //}

        displayMulti();
        EnterAnswer.Select();
        EnterAnswer.ActivateInputField();
        }

        
    public void NewEquation()
    {
        EnterAnswer.text = null;
        FeedbackText.text = null;
        GenerateEquation();
    }

    private void StreakUp(int value)
    {
        streakCurrent += value;
        Debug.Log($"{streakCurrent}");
        if(streakHighest !<= streakCurrent)
        {
            streakHighest = streakCurrent;
        }
    }
    public void resetStreak()
    {
        streakCurrent = 0;
    }

    private void SetMulti(float value)
    {
        multi = value;
    }
    public void resetMulti()
    {
        multi = 1.0f;
    }

    public void displayMulti() // dirty local function should probs call from the ui script.
    {
        multiText.text = $"x{multi:F1}";
    }
    public void displayStreak() // dirty local function should probs call from the ui script.
    {
        StreakText.text = $"{streakCurrent}";
    }
    public IEnumerator PlayStreakFlashVFX(Color colorToFlash, Color colorAfterFlash) {
        float elapsed = 0f;

        while (elapsed < flashDuration * flashRatio)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flashDuration;
            StreakBackground.GetComponent<Image>().color = Color.Lerp(Color.white, colorToFlash, t);
            StreakTimerFillObj.GetComponent<Image>().color = Color.Lerp(Color.white, colorToFlash, t);

            yield return null;
        }

        while (elapsed > flashDuration * flashRatio && elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flashDuration;
            StreakBackground.GetComponent<Image>().color = Color.Lerp(colorToFlash, colorAfterFlash, t);
            StreakTimerFillObj.GetComponent<Image>().color = Color.Lerp(colorToFlash, colorAfterFlash, t);

            yield return null;
        }
    }
    public void PlayAddStreakVFX() { 
        
    }
    private void displayTotalFloor() {
        FloorText.text = $"{totalQuestions}";
    }
    private void ScoreUp(int value, float multiplier = 1)
    {
        Score += value * multiplier;
    }
    private void displayScore() // dirty local function should probs call from the ui script.
    {
        scoreText.text = Score.ToString("0000");
    }

    private void Awake()
    {
        // Pull from menu selection
        operation = GameModeStatic.Operation;
        RandomOperations = GameModeStatic.RandomOperations;
    }

    private void Start()
    {
        Score = 0;
        questionsCorrect = 0;
        totalQuestions = 0;
        streakCurrent = 0;
        EnterAnswer.onEndEdit.AddListener(var => {
            if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
                Debug.Log("End edit on enter");
        });


        GenerateEquation();
        main = Camera.main;
        displayScore();
        displayMulti();
    }

    public void ShowFinaleResults()
    {
        finalScoreText.text = $"{Score}";

        finalQuestionsText.text = "Questions Answered: " + totalQuestions;

        finalCorrectText.text = "Correct: " + questionsCorrect + " / " + totalQuestions;

        float accuracy = 0f;

        if (totalQuestions > 0)
            accuracy = ((float)questionsCorrect / totalQuestions) * 100f;

        finalAccuracyText.text = "Accuracy: " + Mathf.RoundToInt(accuracy) + "%";

        finalBestStreakText.text = "Best Streak: " + streakHighest;

    }
    public float GetCurrentStreak() {
        return streakCurrent;
    }
    private void SetGameMode()
    {

    }


}
