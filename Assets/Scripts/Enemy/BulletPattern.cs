using Core.Extensions;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    public EnemyRanged thisAbility;
    [Header("Projectile Stats")]
    public float bulletSpeed;
    public float damage;

    public virtual void SpawnBulletPattern()
    {
        
    }

    protected virtual void SpawnProjectile(Vector3 target)
    {
        Projectile newProjectile = Instantiate(thisAbility.projectile, thisAbility.thisEntity.transform.position, thisAbility.projectile.transform.rotation);
        newProjectile.transform.Lookat2D(target);
        newProjectile.speed = bulletSpeed;
        newProjectile.damage = damage;
        newProjectile.tag = thisAbility.thisEntity.tag;
    }
}
