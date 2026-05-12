using UnityEngine;

public class ItemPickups : MonoBehaviour
{
    enum itemType { speed, damage, firerate }
    [SerializeField] itemType type;
    [SerializeField] float buffAmount;
    [SerializeField] float buffDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            playerControl player = gameManager.instance.playercontrol;

            if (player != null)
            {
                PlayerBuff activeBuff = player.gameObject.AddComponent<PlayerBuff>();

                activeBuff.Initialize(player, type.ToString(), buffAmount, buffDuration);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Pickup touched player, but gameManager.instance.playercontrol is NULL!");
            }

        }
    }
}
