using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerStartPos.transform.position != transform.position)
        {
            gameManager.instance.playerStartPos.transform.position = transform.position;
            StartCoroutine(checkpointPopup());
        }
    }

    IEnumerator checkpointPopup()
    {
        gameManager.instance.checkPointPopup.SetActive(true);
        yield return new WaitForSeconds(3);
        gameManager.instance.checkPointPopup.SetActive(false);
    }
}
