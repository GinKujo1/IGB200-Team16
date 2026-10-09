using UnityEngine;

public class GravityMod1 : MonoBehaviour
{
    private Rigidbody rb;
    private bool IsSettled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (IsSettled) return;
        if (rb != null && rb.IsSleeping())
        {
            IsRested();
        }
        void IsRested()
        {
            IsSettled = true;
            Debug.Log($"{gameObject.name} has settled!");
            //rb = GetComponent<Rigidbody>();
         //   rb.isKinematic = true;
        }

    }
}
