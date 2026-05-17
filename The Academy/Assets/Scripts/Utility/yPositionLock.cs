using UnityEngine;

public class yPositionLock : MonoBehaviour
{

    float expectedYPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        expectedYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, expectedYPos, transform.position.z);
    }
}
