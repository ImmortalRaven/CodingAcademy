using UnityEngine;

public class walkTowardsPoint : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] GameObject target;
    [SerializeField] string targetName; //*IMPORTANT* This is the name of the object/class that the walking enemy will move towards!
    [SerializeField] Vector3 pointDir; //Direction to move towards
    [SerializeField] float speed; //Flat speed value
    [SerializeField] int turnSpeed; //How fast the enemy turns visually
    [SerializeField] Transform limb;

    float currAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currAngle = 0;
        target = GameObject.Find(targetName);
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //Moves towards the direction of the target every frame
        FaceTarget();
        MoveLimbs();
    }

    void Movement()
    {
        if (target != null)
        {
            pointDir = target.transform.position - gameObject.transform.position;
            pointDir.y = 0;
            controller.Move(pointDir.normalized * speed * Time.deltaTime);
        }
    }

    void FaceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(pointDir.x, 0, pointDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void MoveLimbs()
    {

    }
}
