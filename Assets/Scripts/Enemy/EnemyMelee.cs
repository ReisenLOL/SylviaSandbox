using UnityEngine;

public class EnemyMelee : EntityAbility
{
    public float damage;
    public float minDistanceToPlayer;
    protected override void AbilityEffects()
    {
        if (thisEntity.currentDistanceToPlayer < minDistanceToPlayer)
        {
            RoundManager.instance.player.TakeDamage(damage);
        }
    }
}
