using System;
using UnityEngine;

public class Player : Entity
{
    public Vector3 moveDirection;
    public Vector3 mousePos;
    [SerializeField] private Camera cam;
    public float directionAngle;
    [SerializeField] private Transform healthBarUI;
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
    private void HandleMovement()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

    private void UpdateHealthBar()
    {
        healthBarUI.localScale = new Vector3(health/maxHealth, 1f,1f);
    }
}