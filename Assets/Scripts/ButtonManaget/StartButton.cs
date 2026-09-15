using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Build");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
