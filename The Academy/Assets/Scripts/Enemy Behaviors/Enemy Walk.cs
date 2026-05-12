using UnityEngine;

//HOW TO USE:
/*
 * This script will constantly move towards a specified object, given by targetName. It will continually rotate to face that object and move towards it.
 * 
 * target is currently un-used, but will eventually replace targetName for ease of use and safer functionality.
 * targetName is the name of the object to move towards.
 * pointDir is the direction to move towards, and should not generally be set manually.
 * speed is a flat value for how fast to move.
 * turnSpeed is a flat value for how quickly the enemy should rotate towards the object it is moving towards. 
 * 
 */
public class walkTowardsPoint : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] GameObject target; //This is not currently used.
    [SerializeField] string targetName; //*IMPORTANT* This is the name of the object/class that the walking enemy will move towards!
    [SerializeField] Vector3 pointDir; //Direction to move towards
    [SerializeField] float speed; //Flat speed value
    [SerializeField] int turnSpeed; //How fast the enemy turns visually
    [SerializeField] GameObject activatorObject; //What to watch for activity

    public bool active;

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
        CheckActive();
        if (active)
        {
            Movement(); //Moves towards the direction of the target every frame
            FaceTarget();
        }
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
