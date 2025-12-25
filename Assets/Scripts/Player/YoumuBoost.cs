using System;
using UnityEngine;
using UnityEngine.UI;

public class YoumuBoost : PlayerAbility
{
    public int currentKills;
    public int minKills;
    public int killLimit;

    public float boostTime;
    public float boostLength;
    public float damageBoostAmount;
    public float speedBoostAmount;

    public float boostDelay;
    public float delayTime;

    private Image chargeBar;

    private void Start()
    {
        Enemy.OnKill += UpdateKills;
        chargeBar = GameManager.instance.chargeUI;
    }

    private void UpdateKills()
    {
        if (boostTime <= 0f && delayTime <= 0f && currentKills < killLimit)
        {
            currentKills++;
            chargeBar.transform.localScale = new Vector3(currentKills / (float)killLimit, 1, 1);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (boostTime > 0f)
        {
            chargeBar.color = Color.red;
            boostTime -= Time.deltaTime;
            chargeBar.transform.localScale = new Vector3(boostTime/boostLength, 1, 1);
            if (boostTime < 0f)
            {
                thisPlayer.damageMultiplier = 1f;
                thisPlayer.speedMult = 1f;
                chargeBar.transform.localScale = new Vector3(currentKills / (float)killLimit, 1, 1);
                chargeBar.color = Color.yellow;
                delayTime = boostDelay;
            }
        }
        else if (delayTime > 0f)
        {
            delayTime -= Time.deltaTime;
        }
    }

    protected override void AbilityEffects()
    {
        if (boostTime <= 0f && currentKills > minKills)
        {
            boostTime = boostLength;
            thisPlayer.damageMultiplier = 1f + (damageBoostAmount * (currentKills/(float)killLimit)); 
            thisPlayer.speedMult = speedBoostAmount; //i don't think it should be changed lol. actually maybe give it a minimum so they can't spam the speed
            currentKills = 0;
        }
    }
}
