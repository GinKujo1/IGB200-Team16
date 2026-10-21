using UnityEngine;

public class BuildingCollisionEffects : MonoBehaviour
{
    public SFXType sfx;
    private bool isStacked;

    [SerializeField] private ParticleSystem[] effects;

    private void OnCollisionEnter(Collision collision) {
        if (!isStacked) {
            SoundManager.Instance.PlaySoundEffect(sfx);
            PlayAllEffects();
            isStacked = true;

        }
    }

    public void PlayAllEffects() {
        foreach (ParticleSystem effect in effects)
        {
            if (effect != null)
            {
                //Debug.Log("Playfx");
                effect.Play();

            }
        }
    }
}
