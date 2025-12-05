using System;
using Core.Extensions;
using UnityEngine;

public class StandardPattern : BulletPattern
{
    public Transform[] bulletDirections;
    public Transform bulletParent;
    public bool willLookAtPlayer;

    private void Update()
    {
        if (willLookAtPlayer)
        {
            bulletParent.Lookat2D(RoundManager.instance.player.transform.position);
        }
    }

    public override void SpawnBulletPattern()
    {
        foreach (Transform point in bulletDirections)
        {
            SpawnProjectile(point.position);
        }
    }
}
