using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CorrectBuilding;
    public float heightStep = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SpawnAndMoveUp()
    {
        if (CorrectBuilding != null)
        {
            Instantiate(CorrectBuilding, transform.position, transform.rotation);
            transform.position += new Vector3(0, heightStep, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
