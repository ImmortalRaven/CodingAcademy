using UnityEngine;
using System.Collections;
using System.Data;

public class PlayerBuff : MonoBehaviour
{
    private playerControl player;
    private float duration;
    private string buffType;
    private float amount;
    // Update is called once per frame
    public void Initialize(playerControl playerControl, string type, float buffAmount, float buffDuration)
    {
        player = playerControl;
        buffType = type;
        amount = buffAmount;
        duration = buffDuration;

        ApplyBuff();
    }
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0 )
        {
            RemoveBuff();
            Destroy(this);
        }
    }
    void ApplyBuff()
    {
        if (buffType == "speed") player.ModifySpeed((int)amount);
        if (buffType == "damage") player.ModifyDamage((int)amount);
        if (buffType == "firerate") player.ModifyFireRate(-amount);
    }

    void RemoveBuff()
    {
        if (buffType == "speed") player.ModifySpeed(-(int)amount);
        if (buffType == "damage") player.ModifyDamage(-(int)amount);
        if (buffType == "firerate") player.ModifyFireRate(amount);
    }
}
