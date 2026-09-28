using NUnit.Framework.Internal.Commands;
using System;
using System.Collections;
using Unity.VectorGraphics.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

//List of Names of Audio Clips
public enum SFXType
{
    //Add new audio clip in SFXType Enum, then go to editor to add a new value in the SFXList Array, assign the correct type & audio clip
    SFX_ButtonClick,
    SFX_SectionBuiltSuccessfully,
    LOOP_MainGameBGM,
    AUDIO_MainMenuTransition,
    SFX_OnBuildingSpawned,
    SFX_CorrectAnswer,
    SFX_WrongAnswer,
    SFX_StartButtonClick,
    LOOP_MainGameBGMInrto,
    LOOP_MainMenuLoop,

}
//Structure of Audio Reference
[System.Serializable]
public struct AudioReference { 
    public SFXType sfxType;
    public AudioClip audioClip;
}

//Main Sound Manager Class
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource BGMSource;
    [SerializeField] private AudioReference[] SFXList;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        PlayLoop(SFXType.LOOP_MainGameBGMInrto, SFXType.LOOP_MainGameBGM);
    }
    public AudioClip GetAudioFromSoundList(SFXType type) {
        foreach (var audioReference in SFXList)
        {
            //Find the input type of sound and return the sound clip
            if (audioReference.sfxType == type)
            {
                return audioReference.audioClip;
            }
        }
        Debug.LogError("Sound not found");
        return null;
    }

    public void PlaySoundEffect(SFXType sfxType) {
        AudioClip clip = Instance.GetAudioFromSoundList(sfxType);
        if (clip != null) {
            Instance.audioSource.PlayOneShot(clip);
        }
    }

    private void Update() {
        //Debug key to test sound
        if (Keyboard.current.digit0Key.wasPressedThisFrame || 
            Keyboard.current.digit1Key.wasPressedThisFrame || 
            Keyboard.current.digit2Key.wasPressedThisFrame || 
            Keyboard.current.digit3Key.wasPressedThisFrame || 
            Keyboard.current.digit4Key.wasPressedThisFrame ||
            Keyboard.current.digit5Key.wasPressedThisFrame ||
            Keyboard.current.digit6Key.wasPressedThisFrame ||
            Keyboard.current.digit7Key.wasPressedThisFrame ||
            Keyboard.current.digit8Key.wasPressedThisFrame ||
            Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            PlaySoundEffect(SFXType.SFX_ButtonClick);
        }
    }

    private void PlayLoop(SFXType intro, SFXType mainLoop) {
        AudioClip clip = Instance.GetAudioFromSoundList(intro);
        if (clip != null)
        {
            StartCoroutine(WaitForIntroToEndAndPlayLoop(clip.length, mainLoop));
            Instance.BGMSource.PlayOneShot(clip);
        }

    }
    private void PlayLoop(SFXType Loop) {
        AudioClip clip = Instance.GetAudioFromSoundList(Loop);
        if (clip != null)
        {
            //Instance.BGMSource.Stop();
            Instance.BGMSource.loop = true;
            Instance.BGMSource.clip = clip;
            Instance.BGMSource.Play();
        } 
    }
    private IEnumerator WaitForIntroToEndAndPlayLoop(float introLength, SFXType mainLoop) {

        yield return new WaitForSeconds(introLength - 0.1f);
        PlayLoop(mainLoop);
    }
}
