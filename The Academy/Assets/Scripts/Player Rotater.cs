using UnityEngine;

public class PlayerRotater : MonoBehaviour
{

    private void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 1.0f;
        Vector3 vec = Camera.main.ScreenToWorldPoint(mouse);
        transform.position = new Vector3(vec.x, 0, vec.z);
    }
}
