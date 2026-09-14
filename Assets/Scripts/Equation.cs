using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;



public class Equation : MonoBehaviour
{

    //Assign TMP/Canvas 
    [SerializeField]
    private TMP_Text Number1Text;
    [SerializeField]
    private TMP_Text Number2Text;
    [SerializeField]
    private TMP_Text FeedbackText;
    [SerializeField]
    private TMP_Text OperationTypes;
    [SerializeField]
    private TMP_InputField EnterAnswer;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;


    public int _minValue = 0;
    public int _maxValue = 100;

    public enum Operations { Add, Subtract, Multiply, Division};
    public Operations operation = Operations.Add;

    public bool RandomOperations = false;


    private int Number1;
    private int Number2;
    private int Answer;
    private int UserAnswer;
    private int score = 0;


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
        
        Number1Text.text = Number1.ToString();
        Number2Text.text = Number2.ToString();
        OperationTypes.text = GetOperationSymbol(operation);
    }

    private void GenerateAddition()
    {
        Mathf.Min(_minValue = 0);
        Mathf.Max(_maxValue = 100);

        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 + Number2;
    }
    private void GenerateSubtraction()
    {
        Mathf.Min(_minValue = 0);
        Mathf.Max(_maxValue = 100);


        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 - Number2;
    }
    private void GenerateMultiplication()
    {
        Mathf.Min(_maxValue = 1);
        Mathf.Max(_minValue = 12);

        Number1 = Random.Range(_minValue, _maxValue + 1);
        Number2 = Random.Range(_minValue, _maxValue + 1);
        Answer = Number1 * Number2;
    }
    private void GenerateDivision()
    {
        Mathf.Max(_minValue, 1);
        Mathf.Min(_maxValue, 12); 

        Number2 = Random.Range(_minValue, _maxValue + 1);          
        Answer = Random.Range(_minValue, _maxValue + 1);              
        Number1 = Number2 * Answer;                                  
    }
    
    public void CheckAnswer()
    {
        if (!int.TryParse(EnterAnswer.text, out UserAnswer))
        {
            FeedbackText.text = "Please enter a valid number";
            return;
        }

        if (UserAnswer == Answer)
        {
            FeedbackText.text = "CORRECT";
        }
        else
        {
            FeedbackText.text = $"INCORRECT, the answer was {Answer}.";
        }
    }

        void Update()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            NewEquation();
        }
    }
    public void NewEquation()
    {
        EnterAnswer.text = null;
        FeedbackText.text = null;
        GenerateEquation();
    }
    private void Start()
    {
        GenerateEquation();
    } 
}
