using System.Collections;
using UnityEngine;


public class CameraBehavior : MonoBehaviour
{
    [Header("Initial Values")]
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;
    [SerializeField] private float dampTime = 0.3f;   // Smoothing time
    [SerializeField] private float stopThreshold = 0.1f;


    //[Header("Movement Amount")]
    //[SerializeField] private float cameraMoveAmount = 100f;

    public GameObject spawner;
    private bool isMoving = true;

    private Vector3 velocity = Vector3.zero;

    private Vector3 currentPos;
    private Vector3 targetPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.Translate(0, yOffset, zOffset);
        this.gameObject.transform.Rotate(rotationVector,Space.Self);
        currentPos = this.transform.position;
        targetPos = currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) return;   
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
    
        if (Vector3.Distance(transform.position, targetPos) < stopThreshold && velocity.magnitude < stopThreshold)
        {
            // Snap to exact position and stop tracking movement
            isMoving = false;
            transform.position = targetPos;
            velocity = Vector3.zero;                  
        }
    }
    
    public void SetTargetPosition(float value)
    {
        currentPos = this.transform.position;
        targetPos = currentPos + new Vector3(0, value, 0);
        isMoving = true;
    }

    public void MoveCameraUp()
    {
        currentPos = this.transform.position;
        
        currentPos.y = spawner.transform.position.y;
        targetPos = currentPos;
        isMoving = true;
    }
    private IEnumerator CameraShake(float duration, float magnitude) {
        Vector3 originalpos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration) {
            float x = Random.Range(-1, 1) * magnitude;
            float z = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(originalpos.x + x, originalpos.y, originalpos.z + z);

            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(.004f);
            //yield return null;
        }

        transform.localPosition = originalpos;
    }
    public void StartCameraShake(float duration, float magnitude) {
        StartCoroutine(CameraShake(duration, magnitude));
    }
}
