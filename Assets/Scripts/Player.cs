using System;
using UnityEngine;

public class Player : Entity
{
    public Vector3 moveDirection;
    public Vector3 mousePos;
    [SerializeField] private Camera cam;
    public float directionAngle;
    [SerializeField] private Transform healthBarUI;
    [Header("Animation")] 
    public SpriteRenderer playerSprite;
    public bool isFacingRight = true;
    public Animator animator;
    public string walkAnimTrigger;
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
    private void HandleMovement()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool(walkAnimTrigger, true);
        }
        else
        {
            animator.SetBool(walkAnimTrigger, false);
        }

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
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,cam.nearClipPlane));
    }

    private void Update()
    {
        HandleMovement();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHealthBar();
    }

    protected override void OnKillEffects()
    {
        GameManager.instance.GameOver();
    }

    private void UpdateHealthBar()
    {
        healthBarUI.localScale = new Vector3(health/maxHealth, 1f,1f);
    }
}