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
 */
public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed;
    [SerializeField] int turnSpeed;
    [SerializeField] float seeDist;
    [SerializeField] LayerMask ignoreLayer;

    [SerializeField] GameObject detectObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    enum State {Walking, Turning};

    [SerializeField] int currState;

    int degreesTurned;

    Vector3 myMovementVec;
    void Start()
    {
        myMovementVec = new Vector3(0, 0, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();

        if (currState == (int)State.Walking)
        {
            controller.Move(myMovementVec * Time.deltaTime);
        }
        else if(currState == (int)State.Turning)
        {
            Turning();
        }
    }

    void CheckState()
    {
        RaycastHit wallDetector;
        Debug.DrawRay(transform.position, transform.forward * seeDist, Color.yellow);
        if (Physics.Raycast(transform.position, transform.forward, out wallDetector, seeDist, ~ignoreLayer))
        {
            Debug.Log(wallDetector.collider.name);

            string hitObj = wallDetector.collider.name.Substring(0,4);
            if (hitObj == "Wall")
            {
                currState = (int)State.Turning;
            }
        }
    }

    void Turning()
    {
        if (degreesTurned < 180)
        {
            if (degreesTurned + turnSpeed < 180)
            {
                transform.Rotate(0, turnSpeed, 0);
                degreesTurned += turnSpeed;
            }
            else
            {
                transform.Rotate(0, (180 - degreesTurned), 0);
                degreesTurned = 0;
                currState = (int)State.Walking;
                moveSpeed *= -1;
                myMovementVec = new Vector3(0, 0, moveSpeed);
            }
        }

    }
}
