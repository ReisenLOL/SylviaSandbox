using System;
using Core.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    public Transform shadow;
    [Header("Stall")]
    public bool isStalled;
    public float currentStallTime;
    public float timeStalled;
    [Header("Movement")]
    public float acceleration = 150f;
    public Transform[] possibleDirections;
    private float xSign = -1f;
    private float ySign = 1f;
    [SerializeField] private float forwardPercent;
    [SerializeField] private float strafePercent;
    [Header("Attacks")]
    public EntityAbility mainAttack;
    public float minDistanceToAttack;
    public float currentDistanceToPlayer;
    public float attackDelay;
    public float currentAttackDelayTime;
    public GameObject attackIndicatorDebug;
    public SpriteRenderer sprite;
    private bool isFacingRight = true;

    public static Action OnKill;
    //this entity movement will be basic "follow player"
    private void Start()
    {
        if (Random.Range(0f, 2f) > 1f)
        {
            xSign = -xSign;
            ySign = -ySign;
        }
    }

    protected virtual void Update()
    {
        //if player nearby. start attack wait timer. if stalled, clear attack wait timer and stun
        currentDistanceToPlayer = Vector3.Distance(RoundManager.instance.player.transform.position, transform.position);
        if (isStalled)
        {
            attackIndicatorDebug.SetActive(false);
            currentStallTime -= Time.deltaTime;
            if (currentStallTime <= 0)
            {
                isStalled = false;
            }
        }
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
    private void FlipSprite(Vector2 lookDirection)
    {
        if (lookDirection.x < 0f && isFacingRight)
        {
            sprite.flipX = true;
            shadow.localPosition = new Vector3(-shadow.localPosition.x, shadow.localPosition.y, 0);
            isFacingRight = !isFacingRight;
        }
        else if (lookDirection.x > 0f && !isFacingRight)
        {
            sprite.flipX = false;
            shadow.localPosition = new Vector3(-shadow.localPosition.x, shadow.localPosition.y, 0);
            isFacingRight = !isFacingRight;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log($"Damge: {damage}");
        isStalled = true;
        currentStallTime = timeStalled;
        currentAttackDelayTime = attackDelay;
        GameManager.instance.UpdateHits();
        //also play stun anim
    }

    private void FixedUpdate()
    {
        if (!canMove || isStalled)
        {
            rb.VelocityTowards(Vector2.zero.ScaleToMagnitude(speed), acceleration);
        }
        else
        {
            Vector2 directionToPlayer = (RoundManager.instance.player.transform.position - transform.position).normalized;
            Vector2 directionToMove = directionToPlayer;
            float currentHighestScore = float.NegativeInfinity;
            Vector2 strafeDirection = new Vector3(directionToPlayer.y * ySign, directionToPlayer.x * xSign);
            foreach (Transform direction in possibleDirections)
            {
                Vector3 directionToPoint = (direction.position - transform.position).normalized;
                float forwardScore = Vector3.Dot(directionToPoint, directionToPlayer);
                float strafeScore = Vector3.Dot(directionToPoint, strafeDirection);
                float finalScore = (forwardScore * forwardPercent) + (strafeScore * strafePercent);
                //Debug.Log($"Forward: {forwardScore} Strafe: {strafeScore} Final: {finalScore}");
                if (finalScore > currentHighestScore)
                {
                    //Debug.Log($"{finalScore} is higher than current score: {currentHighestScore}");
                    currentHighestScore = finalScore;
                    directionToMove = directionToPoint;
                }
            }
            rb.VelocityTowards(directionToMove.ScaleToMagnitude(speed), acceleration);
            FlipSprite(directionToMove);
        }
    }
    protected override void OnKillEffects()
    {
        base.OnKillEffects();
        OnKill?.Invoke();
        RoundManager.instance.currentEnemies.Remove(this);
        RoundManager.instance.UpdateEnemyCount();
    }
}
