
using UnityEngine;

public class RandomDirectionFade : MonoBehaviour
{
    [SerializeField] Renderer selfRend;
    [SerializeField] float fadeSpeedMin;
    [SerializeField] float fadeSpeedMax;
    [SerializeField] float moveSpeedMin;
    [SerializeField] float moveSpeedMax;
    [SerializeField] Rigidbody rigidBody;

    float fadeSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeSpeed = Random.Range(fadeSpeedMin, fadeSpeedMax);
        transform.rotation = Random.rotation;
        //transform.rotation = new Quaternion(100f, 0f, 0f, 1);
        rigidBody.linearVelocity = transform.forward * Random.Range(moveSpeedMin, moveSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        Color newColor = new Color(selfRend.material.color.r, selfRend.material.color.g, selfRend.material.color.b, (selfRend.material.color.a - fadeSpeed *Time.deltaTime));
        selfRend.material.color = newColor;
       

        if(selfRend.material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
