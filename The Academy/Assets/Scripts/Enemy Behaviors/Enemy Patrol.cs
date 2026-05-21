using UnityEngine;

//HOW TO USE:
/*
 * This script will cause an enemy to patrol back and forth, and turn when the run into the object set in detectObject. Right now this functionality is not fully implemented, so it is just
 * hardcoded to turn when it detects the "Wall" object in front of it.
 * 
 * moveSpeed is how fast the enemy will move.
 * turnSpeed is how quickly the enemy will turn, when it detects that it needs to turn.
 * seeDist is how far in front of it the object will see with this script.
 * ignoreLayer can be used to ignore any objects on a specific layer when detecting.
 * degreesToTurn is how many degrees the enemy should turn before returning to walking. Good choices are 90 and 180, but it may be customized further.
 * detectObject is not currently used
 * activatorObject is what the enemy will look at to determine if it should be active or not. This should be RoomActivate
 */
public class EnemyPatrol : MonoBehaviour
{
    [Header("----- Movement -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed;
    [SerializeField] int turnSpeed;
    [SerializeField] int degreesToTurn;
    [SerializeField] float seeDist;

    [Header("----- Detection -----")]
    [SerializeField] LayerMask checkLayer;

    [Header("----- Activation (Should Not Be Manually Set) -----")]
    [SerializeField] GameObject activatorObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    enum State {Walking, Turning};

    [SerializeField] int currState;

    int degreesTurned;
    public bool active;

    Vector3 myMovementVec;
    void Start()
    {
        //myMovementVec = new Vector3(0, 0, moveSpeed);
        myMovementVec = transform.forward * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
        if (active)
        {
            CheckState();

            if (currState == (int)State.Walking)
            {
                controller.Move(myMovementVec * Time.deltaTime);
            }
            else if (currState == (int)State.Turning)
            {
                Turning();
            }
        }
    }

    void CheckState()
    {
        RaycastHit wallDetector;
        Debug.DrawRay(transform.position, transform.forward * seeDist, Color.yellow);
        if (Physics.Raycast(transform.position, transform.forward, out wallDetector, seeDist, checkLayer))
        {
            Debug.Log(wallDetector.collider.name);

            string hitObj = wallDetector.collider.name.Substring(0,4);
            if (hitObj != null)
            {
                currState = (int)State.Turning;
            }
        }
    }

    void Turning()
    {
        if (degreesTurned < degreesToTurn)
        {
            if (degreesTurned + turnSpeed < degreesToTurn)
            {
                transform.Rotate(0, turnSpeed, 0);
                degreesTurned += turnSpeed;
            }
            else
            {
                transform.Rotate(0, (degreesToTurn - degreesTurned), 0);
                degreesTurned = 0;
                currState = (int)State.Walking;
                myMovementVec = transform.forward * moveSpeed;
            }
        }

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
