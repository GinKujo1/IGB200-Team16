using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Build");
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartGame() {
        SoundManager.Instance.PlaySoundEffect(SFXType.SFX_StartButtonClick);

        StartCoroutine(LoadingToMainGame(1f));
    }

    public IEnumerator LoadingToMainGame(float transitionTime) {
        StartCoroutine(SoundManager.Instance.FadeOutMusic(transitionTime));
        yield return new WaitForSeconds(transitionTime);
        LoadSceneByIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
