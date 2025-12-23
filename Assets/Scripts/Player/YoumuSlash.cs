using System;
using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

public class YoumuSlash : PlayerAbility
{
    [Header("Stats")] 
    public float damage;
    public float knockbackForce;
    public float movementForce;
    [Header("Cache")]
    public bool canRotate = true;
    public List<Entity> enemiesInRange = new();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            enemiesInRange.Add(isEntity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity) && enemiesInRange.Contains(isEntity))
        {
            enemiesInRange.Remove(isEntity);
        }
    }

    protected override void Update()
    {
        base.Update();
        transform.Lookat2D(thisPlayer.transform.position + thisPlayer.lastMoveDirection);
    }
    protected override void AbilityEffects()
    {
        thisPlayer.rb.AddForce((thisPlayer.lastMoveDirection) * movementForce, ForceMode2D.Impulse);
        foreach (Entity entityFound in enemiesInRange.ToArray())
        {
            entityFound.TakeDamage(damage * thisPlayer.damageMultiplier);
            Vector3 knockbackDirection = (entityFound.transform.position - thisPlayer.transform.position).normalized;
            entityFound.rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); //????
        }
    }
}
