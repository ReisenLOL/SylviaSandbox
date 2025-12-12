using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : Entity
{
    [SerializeField] private AudioClip[] hurtSFX;
    public Vector3 moveDirection;
    public Vector3 lastMoveDirection;
    public Vector3 mousePos;
    [SerializeField] private Camera cam;
    public float directionAngle;
    [Header("Animation")] 
    public SpriteRenderer playerSprite;
    public bool isFacingRight = true;
    public Animator animator;
    public string walkAnimTrigger;
    [Header("Attack Delay")] 
    public bool isStunned;
    public bool isDelayed;
    [SerializeField] private float currentDelayTime;
    [Header("Invulnerability Time")]
    public float invulnLength;
    [SerializeField] private float currentInvulnTime;
    [Header("HealthBar UI")] 
    [SerializeField] private Sprite healthLine;
    [SerializeField] private Sprite depletedHealth;
    [SerializeField] private Image[] healthBar;
    private void FixedUpdate()
    {
        if (!isDelayed)
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }
    private void HandleMovement()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        FlipSprite();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            lastMoveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool(walkAnimTrigger, true);
        }
        else
        {
            animator.SetBool(walkAnimTrigger, false);
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
    }

    private void FlipSprite()
    {
        if (moveDirection.x < 0f && isFacingRight)
        {
            playerSprite.flipX = true;
            isFacingRight = !isFacingRight;
        }
        else if (moveDirection.x > 0f && !isFacingRight)
        {
            playerSprite.flipX = false;
            isFacingRight = !isFacingRight;
        }
    }
    private void Update()
    {
        HandleMovement();
        if (isDelayed)
        {
            currentDelayTime -= Time.deltaTime;
            if (currentDelayTime < 0)
            {
                isDelayed = false;
            }
        }
        if (invulnerable)
        {
            currentInvulnTime -= Time.deltaTime;
            if (currentInvulnTime < 0)
            {
                invulnerable = false;
            }
        }
    }
    public void DelayPlayer(float delayLength)
    {
        isDelayed = true;
        currentDelayTime = delayLength;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();
        if (!invulnerable)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f); //magic numbers but who cares i ain't setting up variables for everything
            audioSource.PlayOneShot(hurtSFX[Random.Range(0, hurtSFX.Length)]);
            invulnerable = true;
            currentInvulnTime = invulnLength;
        }
        
    }

    protected override void OnKillEffects()
    {
        GameManager.instance.GameOver();
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            if (i < health)
            {
                healthBar[i].sprite = healthLine;
            }
            else
            {
                healthBar[i].sprite = depletedHealth;
            }
        }
    }
}