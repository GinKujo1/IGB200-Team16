using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    private void Start() {
        DontDestroyOnLoad(this);
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        switch (scene.buildIndex)
        {
            case 0:
                SoundManager.Instance.PlayLoop(SFXType.AUDIO_MainMenuTransition, SFXType.LOOP_MainMenuLoop, 1);
                break; 
            case 1:
                Debug.Log("Scene is loaded and play music");
                SoundManager.Instance.PlayLoop(SFXType.LOOP_MainGameBGMInrto, SFXType.LOOP_MainGameBGM, 0.2f);
                break;
            default:
                break;
        }
    }


}
