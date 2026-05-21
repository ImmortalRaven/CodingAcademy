using UnityEngine;


//HOW TO USE:
/*
 * set currHealth serialize field to the HP value you want the enemy to have. This script should be added on to enemies.
 * 
 */
public class InanimateTakeDamage : MonoBehaviour, IDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] float currHealth;
    [SerializeField] GameObject key;
    void Start()
    {
        if (key != null)
        {
            gameManager.instance.updateKeyGoal(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amount)
    {
        currHealth -= amount;
        if(currHealth <= 0)
        {
            if (key != null)
            {
                Instantiate(key, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
