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
    [SerializeField] int FoV;


    Vector3 playerDirection;
    float shootTimer;
    float angleToPlayer;
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
            if (PlayerSeen())
            {
                shootTimer += Time.deltaTime;

                if (shootTimer >= shootRate)
                {
                    Shoot();
                }
            }
        }
    }

    bool PlayerSeen()
    {
        bool playerSeen = false;
        playerDirection = gameManager.instance.player.transform.position - transform.position;
        angleToPlayer = Vector3.Angle(playerDirection, transform.forward);

        Debug.DrawRay(transform.position, playerDirection);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDirection, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= FoV)
            {

                playerSeen = true;
            }
        }

        return playerSeen;
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
