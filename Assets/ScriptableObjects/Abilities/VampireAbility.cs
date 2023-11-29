using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vampire Ability", menuName = "ScriptableObjects/Abilities/Vampire")]
public class VampireAbility : AbilitySO
{
    [SerializeField] float healingMultiplier;

    public override void InitCombat(AlienCombat alien)
    {
        alien.OnAfterAttack += Heal;
    }

    private void Heal(AlienCombat attacker, AlienCombat defender, ref int damage)
    {
        attacker.damageTaken -= (int)(damage * healingMultiplier);
        if (attacker.damageTaken < 0)
            attacker.damageTaken = 0;
        attacker.UpdateHealthBar(0, false);
    }
}
