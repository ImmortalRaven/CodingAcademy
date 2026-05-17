using UnityEngine;

public class Fear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == gameManager.instance.player)
        {
            gameManager.instance.playercontrol.ModifyMood(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameManager.instance.player)
        {
            gameManager.instance.playercontrol.ModifyMood(false);
        }
    }
}
