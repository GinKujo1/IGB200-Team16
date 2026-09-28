using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject EquationPanel;
    public static bool isPaused = false;



    void Udate()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else Pause();

        }
    }

    public void Pause()
    {
        EquationPanel.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        EquationPanel.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;


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