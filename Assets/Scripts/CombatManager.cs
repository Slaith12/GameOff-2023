using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<AlienCombat> playerAliens;
    public List<AlienCombat> enemyAliens;

    public void CombatTurn()
    {

    }

    public void CombatStep(AlienCombat attacker, AlienCombat defender)
    {
        //animation code
        defender.damageTaken += (attacker.alienData.defaultAttack + 10 - defender.alienData.defaultDefense) * 4;
        if(defender.damageTaken >= defender.alienData.defaultHealth)
        {
            //defender dies
            Debug.Log("Defender dies");
        }
    }

    public void CombatStepDebug(int id)
    {
        switch(id)
        {
            case 1:
                CombatStep(playerAliens[0], enemyAliens[0]);
                break;
            case 2:
                CombatStep(enemyAliens[0], playerAliens[0]);
                break;
        }
    }
}
