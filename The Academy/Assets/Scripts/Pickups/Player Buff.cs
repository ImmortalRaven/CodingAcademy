using UnityEngine;
using System.Collections;
using System.Data;
using UnityEngine.UI;



public class PlayerBuff : MonoBehaviour
{
    private playerControl player;
    private float duration;
    private string buffType;
    private float amount;
    public bool speedActive = false;
    public bool damageActive = false;
    public bool firerateActive = false;
    public float currentDuration;
    // Update is called once per frame
    public void Initialize(playerControl playerControl, string type, float buffAmount, float buffDuration)
    {
        player = playerControl;
        buffType = type;
        amount = buffAmount;
        duration = buffDuration;
        currentDuration = duration;

        ApplyBuff();
    }
    void Update()
    {
        
        currentDuration -= Time.deltaTime;
        
        updateplayerBUI();
        if (currentDuration <= 0)
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
            player.ModifyFireRate(amount);
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
            player.ModifyFireRate(1 / amount);
            firerateActive = false;
        }
    }

    public void updateplayerBUI()
    {
       
        if (speedActive == true)
        {
            gameManager.instance.speedBG.SetActive(true);

            gameManager.instance.speedPUTimer.fillAmount = currentDuration / duration;
            if (currentDuration <= 0)
            {
                speedActive = false;
                gameManager.instance.speedBG.SetActive(false);
            }
        }
        else if (damageActive == true)
        {
            gameManager.instance.dmgBG.SetActive(true);
            gameManager.instance.dmgPUTimer.fillAmount = currentDuration / duration;
            if (currentDuration <= 0)
            {
                damageActive = false;
                gameManager.instance.dmgBG.SetActive(false);
            }
        }
        else if (firerateActive == true)
        {
            gameManager.instance.firerateBG.SetActive(true);
            gameManager.instance.fireratePUTimer.fillAmount = currentDuration / duration;
            if (currentDuration <= 0)
            {
                firerateActive = false;
                gameManager.instance.firerateBG.SetActive(false);
            }
        }
    }
}
