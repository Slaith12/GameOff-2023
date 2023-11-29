using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Ability", menuName = "ScriptableObjects/Abilities/Buff")]
public class BuffAbility : AbilitySO
{
    [SerializeField] int attackModifier;
    [SerializeField] int defenseModifier;
    [SerializeField] int speedModifier;
    [SerializeField] bool affectBack;

    public override void OnLineupPlace(int index, Alien alien)
    {
        if(index != 0)
        {
            LineupCardContainer buffedCard = LineupManager.instance.lineupCards[index - 1];
            buffedCard.AddAttackModifier(attackModifier);
            buffedCard.AddDefenseModifier(defenseModifier);
            buffedCard.AddSpeedModifier(speedModifier);
        }
        if(affectBack && index != 4)
        {
            LineupCardContainer buffedCard = LineupManager.instance.lineupCards[index + 1];
            buffedCard.AddAttackModifier(attackModifier);
            buffedCard.AddDefenseModifier(defenseModifier);
            buffedCard.AddSpeedModifier(speedModifier);
        }
    }

    public override void OnLineupRemove(int index, Alien alien)
    {
        if (index != 0)
        {
            LineupCardContainer buffedCard = LineupManager.instance.lineupCards[index - 1];
            buffedCard.AddAttackModifier(-attackModifier);
            buffedCard.AddDefenseModifier(-defenseModifier);
            buffedCard.AddSpeedModifier(-speedModifier);
        }
        if (affectBack && index != 4)
        {
            LineupCardContainer buffedCard = LineupManager.instance.lineupCards[index + 1];
            buffedCard.AddAttackModifier(-attackModifier);
            buffedCard.AddDefenseModifier(-defenseModifier);
            buffedCard.AddSpeedModifier(-speedModifier);
        }
    }
}
