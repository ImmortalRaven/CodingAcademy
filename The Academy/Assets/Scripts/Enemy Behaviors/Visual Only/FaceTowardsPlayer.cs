using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FaceTowards : MonoBehaviour
{

    [SerializeField] float turnSpeed;

    Vector3 pointDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointDir = gameManager.instance.player.transform.position - gameObject.transform.position;
        Quaternion rot = Quaternion.LookRotation(pointDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }
}
