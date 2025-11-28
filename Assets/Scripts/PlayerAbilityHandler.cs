using System;
using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour
{
    public PlayerAbility meleeAbility;
    private PlayerAbility meleeAbilityInstance;
    public PlayerAbility areaOfEffectAbility;
    private PlayerAbility areaOfEffectAbilityInstance;
    public PlayerAbility rangedAbility;
    private PlayerAbility rangedAbilityInstance;
    public Transform abilityFolder;
    public Player thisPlayer;
    private void Start()
    {
        meleeAbilityInstance = Instantiate(meleeAbility, abilityFolder);
        meleeAbilityInstance.thisPlayer = thisPlayer;
        areaOfEffectAbilityInstance = Instantiate(areaOfEffectAbility, abilityFolder);
        areaOfEffectAbilityInstance.thisPlayer = thisPlayer;
        rangedAbilityInstance = Instantiate(rangedAbility, abilityFolder);
        rangedAbilityInstance.thisPlayer = thisPlayer;
    }
}
