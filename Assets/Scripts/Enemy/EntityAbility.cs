using UnityEngine;

public class EntityAbility : MonoBehaviour
{
    public Enemy thisEntity; // i realize we probably don't have entities other than enemies.
    [Header("Cooldown")]
    public float abilityCooldown;
    public float currentAbilityCooldown;
    [Header("SFX")] 
    [SerializeField] private AudioClip sound;
    [SerializeField] private float volume;
    protected virtual void Update()
    {
        if (currentAbilityCooldown > 0)
        {
            currentAbilityCooldown -= Time.deltaTime;
        }
    }

    public void ActivateAbility()
    {
        if (currentAbilityCooldown <= 0)
        {
            currentAbilityCooldown = abilityCooldown;
            thisEntity.audioSource.pitch = Random.Range(0.9f, 1.1f);
            thisEntity.audioSource.PlayOneShot(sound, volume);
            AbilityEffects();
        }
    }
    protected virtual void AbilityEffects()
    {
        
    }
}
