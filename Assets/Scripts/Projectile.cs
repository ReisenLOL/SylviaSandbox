using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public float autoDestroyTime;
    private void Start()
    {
        Destroy(gameObject, autoDestroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tag) && other.TryGetComponent(out Entity isEntity))
        {
            isEntity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
