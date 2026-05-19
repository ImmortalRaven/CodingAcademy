using UnityEngine;



public class RoomEnterDetector : MonoBehaviour
{

    [SerializeField] string expectedTag;
    [SerializeField] GameObject doors;

    public bool roomActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        
        if(other.tag == expectedTag)
        {
            roomActive = true;
            if (gameManager.instance.getEnemyCount() > 0)
            {
                doors.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.tag == expectedTag)
        {
            roomActive = false;
        }
    }
}
