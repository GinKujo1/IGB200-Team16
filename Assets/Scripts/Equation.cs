using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;



public class Equation : MonoBehaviour
{
    //Assign TMP/Canvas 
    //[SerializeField]
    //private TMP_Text Number1Text;
    //[SerializeField]
    //private TMP_Text Number2Text;
    //[SerializeField]
    //private TMP_Text OperationTypes;
    [SerializeField]
    private TMP_Text FeedbackText;
    [SerializeField]
    private TMP_InputField EnterAnswer;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;

    [SerializeField]
    private TMP_Text equationText;

    [SerializeField]
    private TMP_Text multiText;
    
    [SerializeField]
    private TMP_Text StreakText;

    private Camera main;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject timerMulti;
    public int _minValue = 0;
    public int _maxValue = 100;

    public enum Operations { Add, Subtract, Multiply, Division};
    public Operations operation = Operations.Add;

    public bool RandomOperations = false;


    private int Number1;
    private int Number2;
    private int Answer;
    private int UserAnswer;
    public float Score { get; private set; } 
    private float multi = 1;
    private float streakCurrent = 1;
    private float streakHighest = 0;
    private int buildingIndex = 0;


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
        Mathf.Min(_minValue);
        Mathf.Max(_maxValue);

        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 + Number2;
    }
    private void GenerateSubtraction()
    {
        Mathf.Min(_minValue);
        Mathf.Max(_maxValue);


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
            FeedbackText.text = "CORRECT";
            SoundManager.Instance.PlaySoundEffect(SFXType.SFX_CorrectAnswer);

            if(timerMulti.GetComponent<Timer_Multiplier>().timeRemaining > 0)
            {
                StreakUp(1);
                displayStreak();
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
            displayScore();
            NewEquation();
        }
        else
        {
            FeedbackText.text = $"INCORRECT, the answer was {Answer}.";
            SoundManager.Instance.PlaySoundEffect(SFXType.SFX_WrongAnswer);
            resetMulti();
            resetStreak();  ///reset streak
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
        if(streakHighest !<= streakCurrent)
        {
            streakHighest = streakCurrent;
        }
    }
    private void resetStreak()
    {
        streakCurrent = 0;
    }

    private void SetMulti(float value)
    {
        multi = value;
    }
    private void resetMulti()
    {
        multi = 1.0f;
    }

    private void displayMulti() // dirty local function should probs call from the ui script.
    {
        multiText.text = $"{multi:F1}x ";
    }
    private void displayStreak() // dirty local function should probs call from the ui script.
    {
        StreakText.text = $"{streakCurrent}";
    }
    private void ScoreUp(int value, float multiplier = 1)
    {
        Score += value * multiplier;
    }
    private void displayScore() // dirty local function should probs call from the ui script.
    {
        scoreText.text = Score.ToString("0000");
    }

    private void Start()
    {
        Score = 0;
        EnterAnswer.onEndEdit.AddListener(var => {
            if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
                Debug.Log("End edit on enter");
        });


        GenerateEquation();
        main = Camera.main;
        displayScore();
        displayMulti();
    } 
 
}
