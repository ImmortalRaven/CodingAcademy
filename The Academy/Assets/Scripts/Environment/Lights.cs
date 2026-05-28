using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] GameObject light1;
    [SerializeField] GameObject light2;
    [SerializeField] GameObject light3;
    [SerializeField] GameObject light4;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(false);
            light4.SetActive(false);
        }
    }
}