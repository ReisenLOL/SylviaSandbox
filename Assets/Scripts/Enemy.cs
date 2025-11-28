using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Attacks")]
    public EntityAbility mainAttack;
    public EntityAbility mainAttackInstance;
    
    //this entity movement will be basic "follow player"
    private void FixedUpdate()
    {
        rb.linearVelocity = (RoundManager.instance.player.transform.position - transform.position).normalized * speed;
    }
    protected override void OnKillEffects()
    {
        base.OnKillEffects();
        RoundManager.instance.currentEnemies.Remove(this);
    }
}
