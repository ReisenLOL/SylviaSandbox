using System;
using UnityEngine;

public class YoumuDefense : PlayerAbility
{
    [SerializeField] private float defenseLength;
    private float currentDefenseTime;

    private void Start()
    {
        Player.onTakeDamage += EndInvulnerability;
    }

    protected override void Update()
    {
        base.Update();
        if (currentDefenseTime > 0)
        {
            currentDefenseTime -= Time.deltaTime;
            if (currentDefenseTime <= 0)
            {
                EndInvulnerability();
            }
        }
    }

    private void EndInvulnerability()
    {
        thisPlayer.invulnerable = false;
        if (currentDefenseTime < 0)
        {
            currentAbilityCooldown =  abilityCooldown * 2f;
        }
        currentDefenseTime = 0;
    }
    protected override void AbilityEffects()
    {
        base.AbilityEffects();
        currentDefenseTime = defenseLength;
        thisPlayer.invulnerable = true;
    }
}
