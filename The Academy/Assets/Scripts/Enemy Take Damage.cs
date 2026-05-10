using UnityEngine;


//HOW TO USE:
/*
 * set currHealth serialize field to the HP value you want the enemy to have. This script should be added on to enemies.
 * 
 */
public class EnemyTakeDamage : MonoBehaviour, IDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] float currHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int amount)
    {
        currHealth -= amount;
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
