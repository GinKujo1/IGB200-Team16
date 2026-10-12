using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Spawner : MonoBehaviour
{
    //public GameObject CorrectBuilding;
    public Transform spawnerObject;
    public float moveUp = 10.0f;
    public GameObject[] BuildingsToSpawn = new GameObject[4];

    public GameObject[] BuildingsToSpawnRed = new GameObject[4];
    public GameObject[] BuildingsToSpawnBlue = new GameObject[4];


    void Update()
    {
        //if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        //{
        //    SpawnBuilding();
        //}
    }

    //public void SpawnBuilding()
    //{
    //    Debug.Log("spawned Building");
    //    SoundManager.Instance.PlaySoundEffect(SFXType.SFX_OnBuildingSpawned);
    //    Vector3 spawnPositon = spawnerObject.position;
    //    Quaternion spawnRotation = spawnerObject.rotation;
    //    Instantiate(CorrectBuilding, spawnPositon, spawnRotation);
    //    transform.position += new Vector3 (0,moveUp,0);
    //}
    /// <summary>
    /// Spawns a building from array. WARNING index cannot exceed the number 3. as you will get an index out of bounds exception error.
    /// </summary>
    /// <param name="index"> do not exceed 3</param>
    public void SpawnBuildingFromlist(int index)
    {   
        Debug.Log("spawned Building");
        SoundManager.Instance.PlaySoundEffect(SFXType.SFX_OnBuildingSpawned);
        Vector3 spawnPositon = spawnerObject.position;
        Quaternion spawnRotation = spawnerObject.rotation;
        Instantiate(BuildingsToSpawn[index], spawnPositon, spawnRotation);
        transform.position += new Vector3(0, moveUp, 0);
    }
}
