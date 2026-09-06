using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject EquationPanel;

    public void Pause()
    {
        EquationPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        EquationPanel.SetActive(true);

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