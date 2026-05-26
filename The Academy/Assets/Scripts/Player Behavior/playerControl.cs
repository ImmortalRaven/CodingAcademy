using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//HOW TO USE:
/*
 * This is the main script that the player uses. It controls movement, health, and attacking with a projectile.
 * 
 * Speed is a flat value for how quickly to move.
 * HP is a flat value for how much health the player will have.
 * shootDMG will be how much damage the bullet fired by the player will do, but is currently not implemented.
 * shootDist can be used if we want to implement raycasting attacks later, but is currently unused.
 * shootRate is how frequently the player can fire, in seconds.
 * shootPoint is where the attacking projectile (bullet) will be fired from.
 * shootSpeed is currently unused, but will eventually be implemented as the speed of the projectile created.
 * shootDir is the direction in which the projectile will be facing when created.
 * shootProjectile is the object to instantiate when firing.
 */
public class playerControl : MonoBehaviour, IDamage
{
    [Header("----- Movement -----")]
    [SerializeField] CharacterController control;
    [SerializeField] int Speed;

    [Header("----- Health -----")]
    [SerializeField] float HP;

    [Header("----- Firing Location -----")]
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform shootDir;

    [Header("----- Primary Attack -----")]
    [SerializeField] int shootDMG;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;
    [SerializeField] float shootSpeed;
    [SerializeField] GameObject shootProjectile;
    [SerializeField] AudioClip shootSound;


    [Header("----- Charge Attack -----")]
    [SerializeField] float chargeShotDMGMax;
    
    [SerializeField] int chargeShotDist;


    [SerializeField] float chargeShotSizeMin;
    [SerializeField] float chargeShotSizeMax;

    [SerializeField] float chargeShotSpeedMax;

    [SerializeField] GameObject chargeBullet;
    [SerializeField] GameObject chargeBulletVisual;

    [SerializeField] AudioClip[] chargeUpSounds;
    [SerializeField] AudioClip[] chargeFireSounds;

    [SerializeField] AudioSource chargeShotSoundHandler;


    

    Vector3 moveDirection;
    float HPOrigin;
    float shootTimer;
    public float shootRateOrig;
    float chargeTimer;

    float chargeShotSize;

    bool fear;
    bool isCharging;

    GameObject myChargeVisual;

    float expectedYPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        expectedYPos = transform.position.y;
        shootRateOrig = shootRate;
        HPOrigin = HP;
        fear = false;
        changePlayerPos();
        myChargeVisual = null;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        movement();
        if (!fear)
        {
            Shoot();
            ChargeShot();
        }
    }

    void movement()
    {
        moveDirection = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        control.Move(moveDirection * Speed * Time.deltaTime);
        LockYPos();
    }

    void Shoot()
    {
        bool mouseDown = Input.GetMouseButton(0);

        if (!isCharging)
        {

            if (!fear)
            {
                if (mouseDown && shootTimer >= shootRate)
                {
                    shootTimer = 0;
                    Quaternion adjustedRot = shootDir.rotation;
                    adjustedRot.y -= 90;
                    GameObject bullet = Instantiate(shootProjectile, shootPoint.position, Quaternion.Euler(0f, shootDir.eulerAngles.y + 90, 0f));
                    bullet.GetComponent<damage>().damageAmount = shootDMG;
                    if (shootSound != null)
                    {
                        AudioSource.PlayClipAtPoint(shootSound, transform.position);
                    }

                }
            }

        }

    }

    
    public void takeDamage(float amount)
    {
        HP -= amount;
        updatePlayerUI();
        
        if(amount < 0)
        {
            StartCoroutine(flashHealScreen());
        }
        else
        {
            StartCoroutine(flashDamageScreen());
        }
        if (HP <= 0)
        {
            gameManager.instance.YouLose();
        }

        if (HP > HPOrigin)
        {
            HP = HPOrigin;
        }

    }
    public void ModifySpeed(int amount)
    {
        Speed += amount;
    }
    public void ModifyDamage(int amount)
    {
        shootDMG += amount;
    }
    public void ModifyFireRate(float amount)
    {
        shootRate /= amount;
        
    }

    public void ModifyMood(bool afraid)
    {
        fear = afraid;
        StartCoroutine(FearScreen());

    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrigin;
    }
    IEnumerator flashDamageScreen()
    {
        gameManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageScreen.SetActive(false);
    }
    IEnumerator flashHealScreen()
    {
        gameManager.instance.playerHealScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerHealScreen.SetActive(false);
    }

    IEnumerator FearScreen()
    {
       gameManager.instance.playerFearFactor.SetActive(true);
        yield return new WaitUntil(() => !fear);
        gameManager.instance.playerFearFactor.SetActive(false);
    }

    void LockYPos()
    {
        transform.position = new Vector3(transform.position.x, expectedYPos, transform.position.z);
    }

    void ChargeShot()
    {
  
            bool mouseDown = Input.GetMouseButton(1);
            


            if (!fear)
            {
                if (mouseDown)
                {
                    isCharging = true;
                    if (myChargeVisual == null) //New charge shot is happening
                    {
                        myChargeVisual = Instantiate(chargeBulletVisual, shootPoint.position, shootPoint.rotation);
                        chargeShotSize = chargeShotSizeMin;
                        chargeShotSoundHandler.PlayOneShot(chargeUpSounds[Random.Range(0, chargeUpSounds.Length - 1)], 0.5f);
                    }
                    chargeTimer += Time.deltaTime;
                    gameManager.instance.ChargeShotBar.fillAmount = chargeShotSize / chargeShotSizeMax;
                    chargeShotSize += Time.deltaTime;

                    myChargeVisual.transform.localScale = new Vector3(chargeShotSize, chargeShotSize, chargeShotSize);
                    myChargeVisual.transform.position = shootPoint.transform.position;



                    if (chargeShotSize > chargeShotSizeMax)
                    {
                        chargeShotSize = chargeShotSizeMax;
                    }
                }

                if (Input.GetMouseButtonUp(1))
                {
                    isCharging = false;
                    Quaternion adjustedRot = shootDir.rotation;
                    adjustedRot.y -= 90;
                    GameObject shotBullet = Instantiate(chargeBullet, shootPoint.transform.position, Quaternion.Euler(0f, shootDir.eulerAngles.y + 90, 0f));

                    shotBullet.transform.localScale = new Vector3(chargeShotSize, chargeShotSize, chargeShotSize);

                    shotBullet.GetComponent<damage>().damageAmount = chargeShotDMGMax * (chargeShotSize / chargeShotSizeMax);
                    shotBullet.GetComponent<damage>().bulletSpeed = (chargeShotSpeedMax * (chargeShotSize / chargeShotSizeMax));
                    shotBullet.GetComponent<TrailRenderer>().startWidth = chargeShotSize;
                    shotBullet.GetComponent<TrailRenderer>().endWidth = chargeShotSize * 0.75f;
                    gameManager.instance.ChargeShotBar.fillAmount = 0;
                    Destroy(myChargeVisual);
                    myChargeVisual = null;

                    chargeShotSoundHandler.Stop();

                    chargeShotSoundHandler.PlayOneShot(chargeFireSounds[Random.Range(0, chargeFireSounds.Length - 1)], 0.5f);
                }
            }

        }
    public void changePlayerPos()
    {
        control.transform.position = gameManager.instance.playerStartPos.transform.position;
        Physics.SyncTransforms();
        HP = HPOrigin;
        updatePlayerUI();
    }
}