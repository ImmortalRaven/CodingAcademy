using UnityEngine;
//HOW TO USE:
/*
 * This script is what is used for rotating the player to face the mouse. To use it, you need the camera associated with the player and a ground/floor object covering all the area the mouse will be in.
 * The ground/floor object must be on the same layer as the groundLayer.
 * This script should be attached to the player BODY, and not the player as a whole, because we do NOT want to rotate the camera, which is part of the player.
 * 
 * mainCamera is the main viewing camera, which should be part of the player prefab.
 * groundLayer is the specified layer that contains the floor/ground object. This should be the Ground layer label.
 */
public class playerAimTest : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask groundLayer;
    Vector3 mouseVec;

    Quaternion mouseQuat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;
  

            if(direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y-90, 0f);
                
            }
        }
    }
}
