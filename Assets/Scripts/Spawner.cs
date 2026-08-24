using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CorrectBuilding;
    public Transform  spawnerObject;
    public void SpawnBuilding()
    {
        if (CorrectBuilding == null || spawnerObject == null)
        {
        }
    
    Vector3 spawnPositon = spawnerObject.position;
    Quaternion spawnRotation = spawnerObject.rotation;
    Instantiate (CorrectBuilding, spawnPositon, spawnRotation);
    }
}
