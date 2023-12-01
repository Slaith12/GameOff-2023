using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProgression : MonoBehaviour
{
    public static EnemyProgression instance;

    public List<EnemyLineupObj> lineups;

    private void Awake()
    {
        instance = this;
    }

    public Alien[] GetEnemyLineup(int round)
    {
        return lineups[round-1].GetLineup();
    }
}
