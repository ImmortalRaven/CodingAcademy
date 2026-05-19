using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Linq;

//HOW TO USE:
/*
 * set currHealth serialize field to the HP value you want the enemy to have. This script should be added on to enemies.
 * 
 */
public class EnemyTakeDamage : MonoBehaviour, IDamage
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] float currHealth;
    [SerializeField] GameObject key;
    [SerializeField] Renderer rend;

    [SerializeField] GameObject spawnedObj;
    [SerializeField] int numToSpawnHit;
    [SerializeField] int numToSpawnDeath;

    Color colorOrig;

    Renderer[] allRenders;
    Color[] allColors;
    void Start()
    {
        colorOrig = rend.material.color;
        gameManager.instance.updateGameGoal(1);
        gameManager.instance.updateEnemyCount(1);
        allRenders = GetComponentsInChildren<Renderer>();
        allColors = new Color[allRenders.Length];
        for (int i = 0; i < allRenders.Length; i++)
            {
                allColors[i] = (allRenders[i].material.color);
            }
        
        if (key != null)
        {
            gameManager.instance.updateKeyGoal(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int amount)
    {

        
        currHealth -= amount;
        MakeGuts(numToSpawnHit);
        if(currHealth <= 0)
        {
            gameManager.instance.updateGameGoal(-1);
            if (key != null)
            {
                Instantiate(key, transform.position, transform.rotation);
            }


            MakeGuts(numToSpawnDeath);
            Destroy(gameObject);

        }
        else
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        rend.material.color = Color.red;
        for(int i = 0; i < allRenders.Length; i++)
        {
            allRenders[i].material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);
        rend.material.color = colorOrig;
        for(int i = 0; i < allRenders.Length; i++)
        {
            allRenders[i].material.color = allColors[i];
        }
        
    }

    private void OnDestroy()
    {
        //MakeGuts();
    }
    public void MakeGuts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(spawnedObj, transform.position, transform.rotation);
        }
        
    }
}
