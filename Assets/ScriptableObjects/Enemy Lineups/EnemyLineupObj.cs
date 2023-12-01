using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Lineup", menuName = "ScriptableObjects/EnemyLineup")]
public class EnemyLineupObj : ScriptableObject
{
    [SerializeField] CardDataSO[] aliens;
    [SerializeField] int[] attackMods;
    [SerializeField] int[] defenseMods;
    [SerializeField] int[] speedMods;

    public Alien[] GetLineup()
    {
        Alien[] lineup = new Alien[aliens.Length];
        for(int i = 0; i < aliens.Length; i++)
        {
            if (aliens[i] == null)
                continue;

            int attackModifier = 0;
            if (attackMods.Length > i)
                attackModifier = attackMods[i];
            int defenseModifier = 0;
            if (defenseMods.Length > i)
                defenseModifier = defenseMods[i];
            int speedModifier = 0;
            if (speedMods.Length > i)
                speedModifier = defenseMods[i];

            lineup[i] = new Alien(aliens[i], attackModifier, defenseModifier, speedModifier);
        }
        return lineup;
    }
}
