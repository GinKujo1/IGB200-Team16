using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void EnterNewScene(int index, SFXType soundToPlay) {
        SceneManager.LoadScene(index);
        SoundManager.Instance.PlaySoundEffect(soundToPlay);
    }
}
