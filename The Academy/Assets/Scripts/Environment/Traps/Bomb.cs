using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] int secToExplosion;
    [SerializeField] int damage;
    [SerializeField] Renderer rend;
    [SerializeField] GameObject warningCircle;
    Color colorOrig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = rend.material.color;
        warningCircle.transform.localScale += new Vector3(radius * 2, 0, radius * 2);
        
        StartCoroutine(explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explosion()
    {
        yield return StartCoroutine(bombFlash());

        Collider[] explosion = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider obj in explosion)
        {
            IDamage dmg = obj.gameObject.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.takeDamage(damage);
            }
            else if (obj.CompareTag("Breakable"))
            {
                Destroy(obj.gameObject);
            }
        }
        Destroy(gameObject);
    }

    IEnumerator bombFlash()
    {
        for (int i = 0; i < secToExplosion; i++)
        {
            rend.material.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = colorOrig;
            yield return new WaitForSeconds(1f);
        }
    }
}
