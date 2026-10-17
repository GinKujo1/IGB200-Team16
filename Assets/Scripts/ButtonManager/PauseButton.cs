using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;


public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;
    public TMP_InputField EnterAnswer;
    public GameObject EquationPanel;
    public static bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
                        Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");

        {
            if (EnterAnswer != null && EnterAnswer.isFocused)
            {
                EnterAnswer.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }


    public void Pause()
    {
        //EquationPanel.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Resume()
    {
        {
            //EquationPanel.SetActive(true);
            PausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }


    public void LoadSceneByName()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}