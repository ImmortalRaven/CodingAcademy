using Unity.VisualScripting;
using UnityEngine;

public class CrushWall : MonoBehaviour
{
    enum direction { X, Y, Z }
    [SerializeField] direction moveDirection;
    [SerializeField] GameObject activatorObject;
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] float moveSpeed;
    [SerializeField] float returnSpeed;


    public bool active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
        if (active)
        {
            Move(end.transform.position, moveSpeed);
        }
        else 
        {
            Move(start.transform.position, returnSpeed);
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
    
    void Move(Vector3 target, float speed)
    {
        if (moveDirection == direction.X && transform.position.x != target.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (moveDirection == direction.Y && transform.position.x != target.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.y, transform.position.z), speed * Time.deltaTime);
        }
        else if (moveDirection == direction.Z && transform.position.x != target.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target.z), speed * Time.deltaTime);
        }
    }

}
