using UnityEngine;

public class GravityMod : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityMultiplier = 2.0f;
    private float sleepinngThreshold = 0.08f;
    private float settleTime = 0.5f;

    private bool hasCollided;
    private bool IsSettle;
    private float stillTimer;

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
        if (IsSettle || !hasCollided || rb == null) return;
        bool isStill = rb.linearVelocity.sqrMagnitude < sleepinngThreshold * sleepinngThreshold;


        // if (rb != null && rb.linearVelocity.sqrMagnitude < sleepinngThreshold * sleepinngThreshold)

        if (isStill)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer >= settleTime)
            {
                Settle();
            }
        }
        else
        {
            stillTimer = 0f;
        }
    }


    // Update is called once per frame
    void FixedUpdate() //Lets me edit the gravity/ force to move the prefab down faster
    {
        if (IsSettle || rb == null) return;


        Vector3 customGravity = Physics.gravity * gravityMultiplier;
        rb.AddForce(customGravity, ForceMode.Acceleration);

    }
    private void OnCollisionEnter(Collision collision)
    {
        hasCollided = true;
    }

    void Settle()
    {
        IsSettle = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

    }
}
