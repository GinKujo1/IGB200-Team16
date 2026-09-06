using UnityEngine;

public class GravityMod : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityMultiplier = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //turns the rigidbody component gravity off 
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
    }

    // Update is called once per frame
    void FixedUpdate() //Lets me edit the gravity/ force to move the prefab down faster
    {
        Vector3 customGravity = Physics.gravity * gravityMultiplier;
        rb.AddForce(customGravity, ForceMode.Acceleration);
        
    }
}
