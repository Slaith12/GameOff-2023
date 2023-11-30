using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Revive Ability", menuName = "ScriptableObjects/Abilities/Revive")]
public class ReviveAbility : AbilitySO
{
    public override void InitCombat(AlienCombat alien)
    {
        alien.OnBeforeDeath += Revive;
    }

    
    private void Revive(AlienCombat alien)
    {
        //Alien is still in lineup at this point, so check for aliens AFTER current one
        if (alien.player)
        {
            if (CombatManager.instance.numPlayerAliens > 1)
                CombatManager.instance.AddPlayerAlien(alien.alienData, 2); //index 0 = dying alien, index 1 = next alien, index 2 = behind next alien
        }
        else
        {
            if (CombatManager.instance.numEnemyAliens > 1)
                CombatManager.instance.AddEnemyAlien(alien.alienData, 2); //index 0 = dying alien, index 1 = next alien, index 2 = behind next alien
        }
    }
}
