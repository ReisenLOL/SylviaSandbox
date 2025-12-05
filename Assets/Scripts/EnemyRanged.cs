using System;
using System.Collections.Generic;
using Core.Extensions;
using Random = UnityEngine.Random;

public class EnemyRanged : EntityAbility
{
    public Projectile projectile;
    public BulletPattern[] patternsList;
    //fine i add it in and remove the instantiation

    protected override void AbilityEffects()
    {
        patternsList[Random.Range(0,patternsList.Length)].SpawnBulletPattern();
    }
}
