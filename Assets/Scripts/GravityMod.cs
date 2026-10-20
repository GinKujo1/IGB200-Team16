using System.Collections;
using UnityEngine;
public class GravityMod : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityMultiplier = 2.0f;

    private bool IsSettle = false;
    private float sleepinngThreshold = 0.05f;
    public float kinematicDelay = 2f;

    private Coroutine settleCoroutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //turns the rigidbody component gravity off 
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        // these are here to attempt to fix the phasing issue.
        rb.solverIterations = 24;
        rb.solverVelocityIterations = 8;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

    }

    void Update()
    {
        if (IsSettle) return;
        if (rb != null && rb.linearVelocity.sqrMagnitude < sleepinngThreshold * sleepinngThreshold)
        {
            StartSettleDelay();
        }

    }



    // Update is called once per frame
    void FixedUpdate() //Lets me edit the gravity/ force to move the prefab down faster
    {
        if (IsSettle || rb == null) return;
        Vector3 customGravity = Physics.gravity * gravityMultiplier;
        rb.AddForce(customGravity, ForceMode.Acceleration);

    }

    void StartSettleDelay()
    {
        if (settleCoroutine != null) return;
        IsSettle = true;
        settleCoroutine = StartCoroutine(TurnKinemtaic());

    }

    private IEnumerator TurnKinemtaic()
    {
       yield return new WaitForSeconds(kinematicDelay);
        if (rb != null)
        {
            IsSettle = true;
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        settleCoroutine = null;
    }


}
