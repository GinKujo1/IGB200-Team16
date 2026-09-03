using NUnit.Framework.Internal.Commands;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

//List of Names of Audio Clips
public enum SFXType
{
    //Add new audio clip in SFXType Enum, then go to editor to add a new value in the SFXList Array, assign the correct type & audio clip
    SFX_ButtonClick,
    SFX_SectionBuiltSuccessfully,
    LOOP_MainGameBGM,
    AUDIO_MainMenuTransition

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
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
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

    public static void PlaySoundEffect(SFXType sfxType) {
        AudioClip clip = Instance.GetAudioFromSoundList(sfxType);
        if (clip != null) {
            Instance.audioSource.PlayOneShot(clip);
        }
    }

    private void Update() {
        //Debug key to test sound
        if (Keyboard.current.anyKey.wasPressedThisFrame) {
            PlaySoundEffect(SFXType.SFX_ButtonClick);
        }
    }

}
