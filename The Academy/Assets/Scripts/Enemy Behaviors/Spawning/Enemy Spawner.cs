using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] GameObject myActivator;
    [SerializeField] LayerMask layerToCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool spawned = false;
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
        Collider[] colliderList = Physics.OverlapBox(transform.position, new Vector3(0.1f, 0.1f, 0.1f), transform.rotation, layerToCheck, QueryTriggerInteraction.Collide);
        for(int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].gameObject.tag == "Activator")
            {
                myActivator = colliderList[i].gameObject;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(myActivator.GetComponent<RoomEnterDetector>().roomActive && spawned == false)
        {
            Instantiate(enemyToSpawn, transform.position, transform.rotation);
            spawned = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Activator")
        {
            myActivator = other.gameObject;
        }
    }
    private void OnTriggerEnter(Collision collision)
    {
        
    }
}
