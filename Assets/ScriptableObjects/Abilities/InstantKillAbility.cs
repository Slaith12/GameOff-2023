using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Instant Kill Ability", menuName = "ScriptableObjects/Abilities/Instant Kill")]
public class InstantKillAbility : AbilitySO
{
    public override void InitCombat(AlienCombat alien)
    {
        alien.OnBeforeAttack += BeforeAttack;
    }

    private void BeforeAttack(AlienCombat attacker, AlienCombat defender, ref int damage)
    {
        if(Random.value <= 0.01)
        {
            defender.damageTaken = defender.alienData.health;
            damage = 9999;
        }
    }
}
