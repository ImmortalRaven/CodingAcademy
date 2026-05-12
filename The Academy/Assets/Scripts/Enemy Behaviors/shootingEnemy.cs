using UnityEngine;

//HOW TO USE:
/*
 * This should be attached to enemies that fire projectiles. It handles the creation of the projectile, including direction and sound effect.
 * 
 * bullet is the object to instantiate when the enemy fires.
 * shootPosition is the position where the bullet will be instantiated.
 * shootRate is how often the enemy will fire, in seconds.
 * gunPivot is the direction that the projectile will be facing when instantiated.
 * shootSound is (optionally) the sound effect to play when firing.
 */
public class shootingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPosition;
    [SerializeField] float shootRate;
    [SerializeField] Transform gunPivot;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject activatorObject;

    float shootTimer;
    public bool active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
        if (active)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootRate)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        shootTimer = 0;
        Instantiate(bullet, shootPosition.position, gunPivot.rotation);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    void CheckActive()
    {
        if (activatorObject != null)
        {
            if (activatorObject.GetComponent<RoomEnterDetector>().roomActive)
            {
                active = true;
            }
            else
            {
                active = false;
            }
        }
        else
        {
            active = true;
        }
    }
}
