using UnityEngine;

public class EntityAbility : MonoBehaviour
{
    public Enemy thisEntity; // i realize we probably don't have entities other than enemies.
    [Header("Cooldown")]
    public float abilityCooldown;
    public float currentAbilityCooldown;
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
            AbilityEffects();
        }
    }
    protected virtual void AbilityEffects()
    {
        
    }
}
