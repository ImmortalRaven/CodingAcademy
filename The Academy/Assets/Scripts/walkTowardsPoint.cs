using UnityEngine;

public class walkTowardsPoint : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] GameObject target;
    [SerializeField] string targetName; //*IMPORTANT* This is the name of the object/class that the walking enemy will move towards!
    [SerializeField] Vector3 pointDir; //Direction to move towards
    [SerializeField] float speed; //Flat speed value

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find(targetName);
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //Moves towards the direction of the target every frame
    }

    void Movement()
    {
        if (target != null)
        {
            pointDir = target.transform.position - gameObject.transform.position;
            controller.Move(pointDir.normalized * speed * Time.deltaTime);
        }
    }
}
