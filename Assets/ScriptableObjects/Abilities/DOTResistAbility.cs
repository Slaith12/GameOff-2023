using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DOT Resist Ability", menuName = "ScriptableObjects/Abilities/DOT Resist")]
public class DOTResistAbility : AbilitySO
{
    public override void InitCombat(AlienCombat alien)
    {
        alien.OnBeforeDamage += ResistDOT;
    }

    private void ResistDOT(AlienCombat attacker, AlienCombat defender, ref int damage)
    {
        //DOT damage uses the non-attacker function, and it's the only one that does that
        //this would be a terrible way to do this if we were going to have more abilities, but it's fine for now :D
        if (attacker == null)
            damage = 0;
    }
}
