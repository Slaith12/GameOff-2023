using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Double Attack Ability", menuName = "ScriptableObjects/Abilities/Double Attack")]
public class DoubleAttackAbility : AbilitySO
{
    [Range(0,1)]
    [SerializeField] float chance;

    bool playerDouble;
    bool enemyDouble;

    public override void InitCombat(AlienCombat alien)
    {
        if(alien.player)
        {
            alien.OnAfterAttack += OnPlayerAttack;
            CombatManager.instance.OnPlayerAttackerChange += delegate { playerDouble = false; };
        }
        else
        {
            alien.OnAfterAttack += OnEnemyAttack;
            CombatManager.instance.OnEnemyAttackerChange += delegate { enemyDouble = false; };
        }
    }

    void OnPlayerAttack(AlienCombat attacker, AlienCombat defender)
    {
        if(playerDouble)
        {
            playerDouble = false;
            return;
        }
        if(Random.value <= chance)
        {
            CombatManager.instance.playerAttackTime = CombatManager.attackTimeThreshold*0.9f;
            playerDouble = true;
        }
    }

    void OnEnemyAttack(AlienCombat attacker, AlienCombat defender)
    {
        if (enemyDouble)
        {
            enemyDouble = false;
            return;
        }
        if (Random.value <= chance)
        {
            CombatManager.instance.enemyAttackTime = CombatManager.attackTimeThreshold * 0.9f;
            enemyDouble = true;
        }
    }
}
