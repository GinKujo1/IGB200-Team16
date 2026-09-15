using UnityEngine;
using UnityEngine.InputSystem;


public class Spawner : MonoBehaviour
{
    public GameObject CorrectBuilding;
    public Transform spawnerObject;
    public float moveUp = 10.0f;


    void Update()
    {
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        //{
        //    SpawnBuilding();
        //}
    }

    public void SpawnBuilding()
    {
        Debug.Log("spawned Building");
        Vector3 spawnPositon = spawnerObject.position;
        Quaternion spawnRotation = spawnerObject.rotation;
        Instantiate(CorrectBuilding, spawnPositon, spawnRotation);
        transform.position += new Vector3 (0,moveUp,0);
    }
}
