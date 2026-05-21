using System.Collections.Generic;
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
    [SerializeField] Transform shootPosition;
    [SerializeField] Transform gunPivot;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject activatorObject;
    [SerializeField] int FoV;

    [SerializeField] List<GunStats> gunList = new List<GunStats>();
    [SerializeField] GameObject gunModel;
    [SerializeField] GunStats startingGun;


    Vector3 playerDirection;


    int gunListPosition = 0;
    float shootTimer;
    float angleToPlayer;
    public bool active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(startingGun != null)
        {
            GetGunStats(startingGun);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
        if (active)
        {
            shootTimer += Time.deltaTime;

            if (PlayerSeen())
            {

                if (shootTimer >= gunList[gunListPosition].shootRate && gunList.Count > 0)
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
        for (int i = 0; i < gunList[gunListPosition].bulletsPerShot; i++)
        {
            GameObject myBullet = Instantiate(gunList[gunListPosition].bullet, shootPosition.position, gunPivot.rotation);

            myBullet.GetComponent<damage>().damageAmount = gunList[gunListPosition].shootDamage;
            myBullet.GetComponent<damage>().bulletSpeed = gunList[gunListPosition].bulletSpeed;

            myBullet.transform.Rotate( Random.Range(-(gunList[gunListPosition].spreadVert), gunList[gunListPosition].spreadVert), Random.Range(-(gunList[gunListPosition].spreadHoriz), gunList[gunListPosition].spreadHoriz), 0);
        }
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    public void GetGunStats(GunStats gunFound)
    {
        gunList.Add(gunFound);
        gunListPosition = gunList.Count - 1;

        ChangeGun();


    }

    void ChangeGun()
    {
        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[gunListPosition].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[gunListPosition].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        gunModel.transform.localScale = gunList[gunListPosition].gunModel.transform.localScale;
    }


    Quaternion BulletSpread(Quaternion initialRot)
    {
        Quaternion newRot = initialRot;

        return newRot;
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
