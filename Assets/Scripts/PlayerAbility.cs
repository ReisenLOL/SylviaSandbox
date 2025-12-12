using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private string abilityAnimTrigger;
    public float delayLength;
    [Header("Cooldown")]
    public float abilityCooldown;
    private float currentAbilityCooldown;
    [Header("Audio")] 
    [SerializeField] private AudioClip sound;
    [SerializeField] private float volume;

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
            thisPlayer.audioSource.pitch = Random.Range(0.9f, 1.1f);
            thisPlayer.audioSource.PlayOneShot(sound, volume);
            if (abilityAnimTrigger != "")
            {
                thisPlayer.animator.SetTrigger(abilityAnimTrigger);
            }
            AbilityEffects();
        }
    }
    protected virtual void AbilityEffects()
    {
        
    }
}
