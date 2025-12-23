using System;
using UnityEngine;
using UnityEngine.UI;

public class YoumuBoost : PlayerAbility
{
    public int currentKills;
    public int killLimit;

    public float boostTime;
    public float boostLength;
    public float damageBoostAmount;

    private Image chargeBar;

    private void Start()
    {
        Enemy.OnKill += UpdateKills;
        chargeBar = GameManager.instance.chargeUI;
    }

    private void UpdateKills()
    {
        if (boostTime <= 0f)
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
                chargeBar.transform.localScale = new Vector3(currentKills / (float)killLimit, 1, 1);
                chargeBar.color = Color.yellow;
            }
        }
    }

    protected override void AbilityEffects()
    {
        if (boostTime <= 0f)
        {
            boostTime = boostLength;
            thisPlayer.damageMultiplier = 1f + (damageBoostAmount * (currentKills/(float)killLimit)); 
            currentKills = 0;
        }
    }
}
