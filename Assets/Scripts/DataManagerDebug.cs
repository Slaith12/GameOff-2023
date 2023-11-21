using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerDebug : MonoBehaviour
{
    [SerializeField] CardDataSO[] playerLineup;
    [SerializeField] CardDataSO[] enemyLineup;

    //this needs to be awake since CombatManager expects DataManager to be set already when the scene is loaded
    private void Awake()
    {
        DataManager data = GetComponent<DataManager>();
        for(int i = 0; i < Mathf.Min(playerLineup.Length, data.playerLineup.Length); i++)
        {
            if (playerLineup[i] == null)
                data.playerLineup[i] = null;
            else
                data.playerLineup[i] = new Alien(playerLineup[i]);
        }
        for (int i = 0; i < Mathf.Min(enemyLineup.Length, data.enemyLineup.Length); i++)
        {
            if (enemyLineup[i] == null)
                data.enemyLineup[i] = null;
            else
                data.enemyLineup[i] = new Alien(enemyLineup[i]);
        }
    }
}
