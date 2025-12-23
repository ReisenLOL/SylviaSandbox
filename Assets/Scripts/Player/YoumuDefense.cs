using System;
using UnityEngine;

public class YoumuDefense : PlayerAbility
{
    [SerializeField] private float defenseLength;
    private float currentDefenseTime;

    private void Start()
    {
        //Player.onTakeDamage += EndInvulnerability; not yet man
    }

    protected override void Update()
    {
        base.Update();
        if (currentDefenseTime > 0)
        {
            /*
            thisPlayer.invulnerable = true;
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.V))
            {
                thisPlayer.canMove = false;
            }
            else if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.V))
            {
                thisPlayer.canMove = true; //this code fucking sucks dude
            }
            else
            {

            }
            */
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
        thisPlayer.canMove = true;
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
