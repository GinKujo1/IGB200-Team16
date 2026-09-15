using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public GameObject ResultPanel;
    public GameObject EquationPanel;

    public void LoadSceneByName()
    {
        SceneManager.LoadScene("HomeScreen");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

}