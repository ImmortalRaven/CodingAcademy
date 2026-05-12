using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject healthPiece;
    [SerializeField] Canvas displayCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int currentHealth;

    private void Start()
    {
        currentHealth = 0;
        UpdateHealth(7);
        DisplayHealth();
    }

    void UpdateHealth(int amount)
    {
        currentHealth += amount;
    }

    void DisplayHealth()
    {
        Transform myTransform = displayCanvas.transform;
        Vector3 modifyX = new Vector3(-120, 0, 0);
        Vector3 modifyY = new Vector3(0, -120, 0);
        myTransform.position += modifyY;
        for (int i = 0; i < currentHealth; i++)
        {
            myTransform.position += modifyX;
            Instantiate(healthPiece, Vector3.zero, Quaternion.identity, displayCanvas.transform);
        }
    }
}
