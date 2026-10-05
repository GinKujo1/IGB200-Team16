using UnityEngine;

public class MaterialRandomiser : MonoBehaviour
{
    [SerializeField]private Material[] mats = new Material[5]; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer meshR = GetComponent<MeshRenderer>();
        meshR.material = mats[Random.Range(0,4)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
