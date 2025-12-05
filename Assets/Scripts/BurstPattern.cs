using System.Collections;
using UnityEngine;

public class BurstPattern : BulletPattern
{
    public float burstDelay;
    public int burstCount;
    public override void SpawnBulletPattern()
    {
        StartCoroutine(BurstFire());
    }

    IEnumerator BurstFire()
    {
        int currentCount = 0;
        Vector3 playerPos = RoundManager.instance.player.transform.position;
        while (currentCount < burstCount)
        {
            SpawnProjectile(playerPos);
            currentCount++;
            yield return new WaitForSeconds(burstDelay);
        }
        yield break;
    }
}
