using UnityEngine;
using System.Collections;
using System.Data;
using System.Linq.Expressions;


public class PlayerBuff : MonoBehaviour
{
    private playerControl player;
    private float duration;
    private string buffType;
    private float amount;
    public bool speedActive = false;
    public bool damageActive = false;
    public bool firerateActive = false;
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
        if (buffType == "speed")
        {
            player.ModifySpeed((int)amount);
            speedActive = true;
}

        if (buffType == "damage")
        {
            player.ModifyDamage((int)amount);
            damageActive = true;
        }
        if (buffType == "firerate")
        {
            player.ModifyFireRate(-amount);
            firerateActive = true;
        }
    }

    void RemoveBuff()
    {
        if (buffType == "speed")
        {
            player.ModifySpeed(-(int)amount);
            speedActive = false;
        }

        if (buffType == "damage")
        {
            player.ModifyDamage(-(int)amount);
            damageActive = false;
        }

        if (buffType == "firerate")
        {
            player.ModifyFireRate(amount);
            firerateActive = false;
        }
    }
}
