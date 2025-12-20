using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

public class YoumuAOE : PlayerAbility
{
    [Header("Stats")] 
    public float damage;
    public float knockbackForce;
    [Header("Cache")]
    public bool canRotate = true;
    public List<Entity> enemiesInRange = new();
    public DebugEffect attackEffect;
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
    }
    protected override void AbilityEffects()
    {
        Instantiate(attackEffect, thisPlayer.transform);
        foreach (Entity entityFound in enemiesInRange.ToArray())
        {
            entityFound.TakeDamage(damage);
            Vector3 knockbackDirection = (entityFound.transform.position - thisPlayer.transform.position).normalized;
            entityFound.rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); //????
        }
    }
}
