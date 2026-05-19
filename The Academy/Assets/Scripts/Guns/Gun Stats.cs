using UnityEngine;


[CreateAssetMenu]

public class GunStats : ScriptableObject
{
    public GameObject gunModel;


    [Range(1, 30)] public int shootDamage;
    [Range(5, 500)] public int bulletLifetime;
    [Range(0.05f, 3f)] public float shootRate;
    [Range(1, 20)] public int bulletsPerShot;
    [Range(0, 90)] public int spreadHoriz;
    [Range(0, 10)] public int spreadVert;
    [Range(0.2f, 100f)] public float bulletSpeed;
    public GameObject bullet;

    public bool needsAmmo;
    public int ammoCurr;
    [Range(5, 80)] public int ammoMax;

    public ParticleSystem hitEffect;
    public AudioClip[] shootSound;
    [Range(0, 1)] public float shootSoundVolume;

}
