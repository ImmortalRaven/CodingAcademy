using UnityEditor;
using UnityEngine;

public class LimbWiggle : MonoBehaviour
{

    [SerializeField] float maxWiggle;
    [SerializeField] float minWiggle;
    [SerializeField] float wiggleSpeed;
    [SerializeField] int timeToTurn;

    float currAngle;
    int currTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(-wiggleSpeed*timeToTurn, 0, 0);
        currAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RotateSelf();
    }

    void RotateSelf()
    {
        currTime+= 1;
        if(transform.rotation.x <= minWiggle || transform.rotation.x >= maxWiggle)
        {
            wiggleSpeed *= -1;
        }
        if (currTime >= timeToTurn*2)
        {
            wiggleSpeed *= -1;
            currTime = 0;
        }



        transform.Rotate(wiggleSpeed, 0, 0);
      
        
        
    }
}
