using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Revive Ability", menuName = "ScriptableObjects/Abilities/Revive")]
public class ReviveAbility : AbilitySO
{
    public override void InitCombat(AlienCombat alien)
    {
        alien.OnAfterDeath += Revive;
    }

    
    private void Revive(AlienCombat alien)
    {
        if (alien.player)
        {
            if (CombatManager.instance.numPlayerAliens > 0)
                CombatManager.instance.AddPlayerAlien(alien.alienData, 1); //index 0 = next alien, index 1 = behind next alien
        }
        else
        {
            if (CombatManager.instance.numEnemyAliens > 0)
                CombatManager.instance.AddEnemyAlien(alien.alienData, 1); //index 0 = next alien, index 1 = behind next alien
        }
    }
}
