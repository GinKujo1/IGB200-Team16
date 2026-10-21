using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PreGameCountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    [SerializeField] private GameObject countDownUIObj;
    [SerializeField] private Timer timerObj;
    [SerializeField] private GameObject EquationPanel;

    void Start() {
        EquationPanel.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown() {
        string[] steps = { "3", "2", "1", "GO!" };

        foreach (string step in steps)
        {
            countdownText.text = step;
            yield return new WaitForSeconds(1f);
        }

        countDownUIObj.gameObject.SetActive(false);
        timerObj.StartTimer();
        EquationPanel.SetActive(true);

    }
}
