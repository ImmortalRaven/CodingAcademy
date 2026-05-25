using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GunStats gun;

    private void OnTriggerEnter(Collider other)
    {
        IPickup pickup = other.GetComponent<IPickup>();

        if (pickup != null)
        {
            gun.ammoCurr = gun.ammoMax;
            pickup.GetGunStats(gun);
            Destroy(gameObject);
        }
    }
}

