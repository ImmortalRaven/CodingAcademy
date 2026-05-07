using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField] CharacterController control;
    [SerializeField] int Speed;


    Vector3 moveDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        moveDirection = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        control.Move(moveDirection * Speed * Time.deltaTime);
    }
}