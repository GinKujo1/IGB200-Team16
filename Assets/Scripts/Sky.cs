using System.Collections;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Material blendedSkyboxMaterial;
    [SerializeField] private float transitionDuration = 2.0f;

    private Coroutine transitionCoroutine;

    private void Start()
    {
        if (blendedSkyboxMaterial != null)
        {
            blendedSkyboxMaterial.SetFloat("_Blend", 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Build"))
        {
            StartTransition(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Build"))
        {
            StartTransition(0f); 
        }
    }

    private void StartTransition(float targetValue)
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        transitionCoroutine = StartCoroutine(TransitionSkybox(targetValue));
    }

    private IEnumerator TransitionSkybox(float targetBlend)
    {
        float startBlend = blendedSkyboxMaterial.GetFloat("_Blend");
        float time = 0;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            float currentBlend = Mathf.Lerp(startBlend, targetBlend, time / transitionDuration);
            
            blendedSkyboxMaterial.SetFloat("_Blend", currentBlend);
            
            DynamicGI.UpdateEnvironment(); 
            
            yield return null;
        }

        blendedSkyboxMaterial.SetFloat("_Blend", targetBlend);
        DynamicGI.UpdateEnvironment();
    }

    private void OnDestroy()
    {
        if (blendedSkyboxMaterial != null)
        {
            blendedSkyboxMaterial.SetFloat("_Blend", 0f);
        }
    }
}
