using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


public class Equation : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Number1Text;
    [SerializeField]
    private TMP_Text Number2Text;
    [SerializeField]
    private TMP_Text FeedbackText;
    [SerializeField]
    private TMP_InputField EnterAnswer;
    public int _minValue = 0;
    public int _maxValue = 100;

    private int Number1;
    private int Number2;
    private int Answer;
    private int UserAnswer;



    private int RandomNumber()
    {
        return Random.Range(_minValue, _maxValue + 1);
    }


    private void GenerateEquation()
    {
        Number1 = RandomNumber();
        Number2 = RandomNumber();
        Answer = Number1 + Number2;

        Number1Text.text = Number1.ToString();
        Number2Text.text = Number2.ToString();
    }

    public void CheckAnswer()
    {
        UserAnswer = int.Parse(EnterAnswer.text);

        if(UserAnswer == Answer)
        {
            FeedbackText.text = "CORRECT";
        }
        else
        {
            FeedbackText.text = "INCORRECT, the answer was {Answer}.";     
        }
    }
        
    public void NewEquation()
    {
        EnterAnswer.text = null;
        FeedbackText.text = null;
        GenerateEquation();
    }
}
