using UnityEngine;

public class GutSplosion : MonoBehaviour
{
    [SerializeField] GameObject spawnedObj;
    [SerializeField] int numToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeGuts()
    {
        for(int i = 0; i < numToSpawn; i++)
        {
            Instantiate(spawnedObj, transform.position, transform.rotation);
        }
    }
}
