using UnityEngine;

public class playerControl : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController control;
    [SerializeField] int Speed;
    [SerializeField] int HP;
    [SerializeField] int shootDMG;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;


    Vector3 moveDirection;
    int HPOrigin;
    float shootTimer;

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

    void shoot()
    {

    }

    0
    public void takeDamage(int amount)
    {
        HP -= amount;

    }
}