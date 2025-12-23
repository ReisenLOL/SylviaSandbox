using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAbilityHandler : MonoBehaviour
{
    public PlayerAbility meleeAbility;
    private PlayerAbility meleeAbilityInstance;
    public PlayerAbility areaOfEffectAbility;
    private PlayerAbility areaOfEffectAbilityInstance;
    public PlayerAbility rangedAbility;
    private PlayerAbility rangedAbilityInstance;
    public PlayerAbility boostAbility;
    private PlayerAbility boostAbilityInstance;
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
        boostAbilityInstance = Instantiate(boostAbility, abilityFolder);
        boostAbilityInstance.thisPlayer = thisPlayer;
    }
    private void Update()
    {
        if (thisPlayer.canAttack)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
            {
                meleeAbilityInstance.ActivateAbility();
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
            {
                areaOfEffectAbilityInstance.ActivateAbility();
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.C))
            {
                rangedAbilityInstance.ActivateAbility();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.V))
        {
            boostAbilityInstance.ActivateAbility(); //i think they should be able to defend at any time?
        }
    }
}
