using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public CanvasGroup fadeOutImage;
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
    public IEnumerator FadeOutScreen(float fadeTime) {
        float startAlphaValue = fadeOutImage.alpha;
        float t = 0f;
        while (fadeOutImage.alpha < 1)
        {
            Debug.Log("Fading");
            fadeOutImage.alpha += Time.deltaTime;
            //fadeOutImage.alpha += startAlphaValue * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator LoadingToMainGame(float transitionTime) {
        StartCoroutine(SoundManager.Instance.FadeOutMusic(transitionTime));
        StartCoroutine(FadeOutScreen(transitionTime));
        yield return new WaitForSeconds(transitionTime);
        LoadSceneByIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
