using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public GameObject ResultPanel;
    public GameObject EquationPanel;

    public void LoadSceneByName()
    {
        //SceneManager.LoadScene("HomeScreen");
        SceneManager.LoadScene(0);
    
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

}