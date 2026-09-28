using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject EquationPanel;
    public static bool isPaused = false;


    public void Pause()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EquationPanel.SetActive(false);
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void Resume()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EquationPanel.SetActive(true);
            PausePanel.SetActive(false);
            Time.timeScale = 10f;
            isPaused = false;
        }
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}