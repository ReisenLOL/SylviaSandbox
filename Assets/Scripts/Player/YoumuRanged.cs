using System.Collections;
using Core.Extensions;
using UnityEngine;

public class YoumuRanged : PlayerAbility
{
    public Projectile projectile; 
    public float burstTime;
    public int burstAmount;
    protected override void AbilityEffects()
    {
        StartCoroutine(BurstFire());
    }

    IEnumerator BurstFire()
    {
        int currentBurst = 0;
        while (currentBurst < burstAmount)
        {
            Projectile newProjectile = Instantiate(projectile, transform.position, projectile.transform.rotation);
            newProjectile.transform.Lookat2D(thisPlayer.transform.position + thisPlayer.lastMoveDirection);
            newProjectile.tag = thisPlayer.tag;
            newProjectile.damage *= thisPlayer.damageMultiplier;
            currentBurst++;
            yield return new WaitForSeconds(burstTime);
        }
        yield break;
    }
}
