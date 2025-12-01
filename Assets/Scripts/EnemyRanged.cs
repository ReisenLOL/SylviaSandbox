using Core.Extensions;
using UnityEngine;

public class EnemyRanged : EntityAbility
{
    public Projectile projectile;
    protected override void AbilityEffects()
    {
        Projectile newProjectile = Instantiate(projectile, thisEntity.transform.position, projectile.transform.rotation);
        newProjectile.transform.Lookat2D(RoundManager.instance.player.transform.position);
        newProjectile.tag = thisEntity.tag;
    }
}
