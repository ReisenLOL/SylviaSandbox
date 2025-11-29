using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Health")] 
    public float health;
    public float maxHealth;
    public float defense;
    public bool invulnerable;
    //public DamageNumberSO onHitDamageNumber;
    [Header("Stats")]
    public float speed;
    [Header("Flags")]
    public bool canAttack = true;
    public bool canMove = true;
    
    [Header("CACHE")]
    public Rigidbody2D rb;
    public AudioSource audioSource;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private float damageColorChangeSpeed;
    public virtual void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            health -= damage - defense; //unsure if it should be a percentage or a flat value.
            StartCoroutine(DamageAnimation());
            //onHitDamageNumber.Spawn(transform.position, damage);
            if (health <= 0)
            {
                OnKillEffects();
            }
        }
    }
    public virtual void Heal(float healing)
    {
        health += healing;
        health = Mathf.Clamp(health, 0f, maxHealth);
    }
    protected virtual void OnKillEffects()
    {
        Destroy(gameObject);
    }
    private IEnumerator DamageAnimation()
    {
        float currentState = 0;
        while (currentState < 1)
        {
            currentState += Time.deltaTime * damageColorChangeSpeed;
            foreach (SpriteRenderer spritepart in spriteRenderers)
            {
                if (spritepart)
                {
                    spritepart.color = Color.Lerp(Color.gray, Color.white, currentState);
                }
            }
            yield return null;
        }
    }
}