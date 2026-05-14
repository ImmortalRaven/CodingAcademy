using UnityEngine;

public class KeyPickup : MonoBehaviour
{
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
        if (other.CompareTag("Player"))
        {
            if(gameManager.instance.playercontrol != null)
            {
                gameManager.instance.updateKeyGoal(-1);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Key was touched but not picked up");
            }
        }
    }
}
