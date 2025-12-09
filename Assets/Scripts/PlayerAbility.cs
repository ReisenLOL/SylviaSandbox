using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float delayLength;
    [Header("Cooldown")]
    public float abilityCooldown;
    private float currentAbilityCooldown;

    [HideInInspector] public Player thisPlayer;
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
            thisPlayer.DelayPlayer(delayLength);
            AbilityEffects();
        }
    }
    protected virtual void AbilityEffects()
    {
        
    }
}
