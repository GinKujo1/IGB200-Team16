using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public SFXType sfx;
    private bool isStacked;


    private void OnCollisionEnter(Collision collision) {
        if (!isStacked) {
            SoundManager.Instance.PlaySoundEffect(sfx);
            isStacked = true;
        }
    }
}
