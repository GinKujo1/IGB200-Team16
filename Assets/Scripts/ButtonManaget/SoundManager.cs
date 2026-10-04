using NUnit.Framework.Internal.Commands;
using System;
using System.Collections;
using Unity.VectorGraphics.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

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
    [SerializeField] AudioSource BGMIntroSource;

    AudioSource sourceRef;
    [SerializeField] private AudioReference[] SFXList;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //PlayLoop(SFXType.LOOP_MainGameBGMInrto, SFXType.LOOP_MainGameBGM);
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

    public void PlayLoop(SFXType intro, SFXType mainLoop, float volume) {
        double dspNow = AudioSettings.dspTime;
        double startIntroAt = dspNow;
        BGMIntroSource.volume = volume;
        BGMSource.volume = volume;

        AudioClip introClip = GetAudioFromSoundList(intro);
        AudioClip mainLoopClip = GetAudioFromSoundList(mainLoop);

        // Intro starts scheduled
        BGMIntroSource.clip = introClip;
        BGMIntroSource.PlayScheduled(startIntroAt);

        // Loop starts exactly when intro ends
        double loopStartAt = startIntroAt + introClip.length;

        BGMSource.loop = true;
        BGMSource.clip = mainLoopClip;
        BGMSource.PlayScheduled(loopStartAt);
    }
    public void PlayLoop(SFXType Loop) {
        AudioClip clip = Instance.GetAudioFromSoundList(Loop);
        if (clip != null)
        {
            //Instance.BGMSource.Stop();
            Instance.BGMSource.loop = true;
            Instance.BGMSource.clip = clip;
            Instance.BGMSource.Play();
        } 
    }

    public IEnumerator FadeOutIntro(float FadeTime) {
        float startVolume = BGMIntroSource.volume;
        if (!BGMIntroSource.isPlaying)
            yield break;
     
        while (BGMIntroSource.volume > 0)
        {
            BGMIntroSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        BGMIntroSource.Stop();
        BGMIntroSource.volume = startVolume;
    }
    public IEnumerator FadeOutMusic(float FadeTime) {
        if (BGMIntroSource.isPlaying) {
            sourceRef = BGMIntroSource;
        } else if(BGMSource.isPlaying) {
            sourceRef = BGMSource;
        }

        float startVolume = sourceRef.volume;
        float t = 0f;
        while (sourceRef.volume > 0)
        {
            t += Time.deltaTime;
            //sourceRef.volume = Mathf.Lerp(startVolume, 0f, t / FadeTime);
            sourceRef.volume -= startVolume * Time.deltaTime / FadeTime;
            Debug.Log("Fading out music");
            yield return null;
        }

        sourceRef.Stop();
        sourceRef.volume = startVolume;
    }
}
