using System;
using UnityEngine;

public class Player : Entity
{
    public Vector3 moveDirection;
    public Vector3 mousePos;
    [SerializeField] private Camera cam;
    public float directionAngle;
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
}