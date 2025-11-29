using System;
using Core.Extensions;
using UnityEngine;

public class Enemy : Entity
{
    public float acceleration = 150f;
    private Vector2 lookDirection;
    [Header("Attacks")]
    public EntityAbility mainAttack;
    public EntityAbility mainAttackInstance;
    //this entity movement will be basic "follow player"
    private void Update()
    {
        lookDirection = (RoundManager.instance.player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.VelocityTowards(lookDirection.ScaleToMagnitude(speed), acceleration);
    }
    protected override void OnKillEffects()
    {
        base.OnKillEffects();
        RoundManager.instance.currentEnemies.Remove(this);
    }
}
