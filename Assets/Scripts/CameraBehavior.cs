
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private float xRotation = 15f;
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.Translate(0, yOffset, zOffset);
        this.gameObject.transform.Rotate(rotationVector,Space.Self);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
