using UnityEngine;

public class FearBody : MonoBehaviour
{

    [SerializeField] GameObject enemyToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
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
        }
    }

    private void OnDestroy()
    {
        gameManager.instance.playercontrol.ModifyMood(false);
    }
}
