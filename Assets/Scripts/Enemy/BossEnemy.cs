using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private EntityAbility[] attacks;
    private bool canBeStalled = true;
    private int currentMaxHits;
    private int currentHits;
    [SerializeField] private int hitLimit;
    [SerializeField] private int hitBaseLimit;
    [SerializeField] private float stallTimeout;
    private float stallTime;

    private void Start()
    {
        currentMaxHits = Random.Range(hitBaseLimit, hitLimit);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        currentHits++;
        if (currentHits >= hitLimit)
        {
            currentHits = 0;
            currentMaxHits = Random.Range(hitBaseLimit, hitLimit); //pasted code because too lazy to make a new function lol
            canBeStalled = false;
            isStalled = false;
        }
        if (canBeStalled)
        {
            isStalled = true;
            currentStallTime = timeStalled;
            currentAttackDelayTime = attackDelay;
            GameManager.instance.UpdateHits();
        }
    }
    protected override void Update()
    {
        currentDistanceToPlayer = Vector3.Distance(RoundManager.instance.player.transform.position, transform.position);
        if (canBeStalled)
        {
            stallTime -= Time.deltaTime;
            if (stallTime <= 0)
            {
                stallTime = stallTimeout;
                canBeStalled = false; //this should work? also this code looks awful!
                
            }
        }
        if (isStalled)
        {
            attackIndicatorDebug.SetActive(false);
            currentStallTime -= Time.deltaTime;
            if (currentStallTime <= 0)
            {
                isStalled = false;
            }
        } //to prevent the player from infinitely stalling the boss we need to figure out how to like. limit the length to be stalled for.
        //perhaps we could set the max hit amount?
        else if (!isStalled && (currentDistanceToPlayer < minDistanceToAttack || currentAttackDelayTime < attackDelay) && mainAttack.currentAbilityCooldown < 0) //and check if attack isn't on cooldown. nvm we can just add a bool check, probably faster than a float comparison. nvm it would just negate the attack delay
        {
            attackIndicatorDebug.SetActive(true);
            canMove = false;
            currentAttackDelayTime -= Time.deltaTime;
            if (currentAttackDelayTime < 0)
            {
                attackIndicatorDebug.SetActive(false);
                canMove = true;
                currentAttackDelayTime = attackDelay;
                mainAttack.ActivateAbility();
            }
        }
    }
}
