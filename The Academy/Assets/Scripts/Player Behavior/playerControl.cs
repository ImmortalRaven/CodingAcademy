using UnityEngine;

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
    [SerializeField] CharacterController control;
    [SerializeField] int Speed;
    [SerializeField] int HP;
    [SerializeField] int shootDMG;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootSpeed;
    [SerializeField] Transform shootDir;
    [SerializeField] GameObject shootProjectile;
    [SerializeField] AudioClip shootSound;


    Vector3 moveDirection;
    int HPOrigin;
    float shootTimer;
    public float shootRateOrig;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootRateOrig = shootRate;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        movement();
        Shoot();
    }

    void movement()
    {
        moveDirection = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        control.Move(moveDirection * Speed * Time.deltaTime);
    }

    void Shoot()
    {
        bool mouseDown = Input.GetMouseButton(0);

        if (mouseDown && shootTimer >= shootRate)
        {
            shootTimer = 0;
            Quaternion adjustedRot = shootDir.rotation;
            adjustedRot.y -= 90;
            GameObject bullet = Instantiate(shootProjectile, shootPoint.position, Quaternion.Euler(0f, shootDir.eulerAngles.y + 90, 0f));
            bullet.GetComponent<damage>().damageAmount = shootDMG;
            if(shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
            }
            
        }

    }

    
    public void takeDamage(int amount)
    {
        HP -= amount;

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
        shootRate += amount;
        if (shootRate < 0.1f) shootRate = 0.1f;
        if (shootRate > shootRateOrig) shootRate = shootRateOrig;
    }
}