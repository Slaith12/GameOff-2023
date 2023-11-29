using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProgression : MonoBehaviour
{
    public static EnemyProgression instance;

    [SerializeField] private List<CardDataSO> lineup1 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup2 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup3 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup4 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup5 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup6 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup7 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup8 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup9 = new List<CardDataSO>();
    [SerializeField] private List<CardDataSO> lineup10 = new List<CardDataSO>();

    private void Awake()
    {
        instance = this;
    }

    public List<CardDataSO> GetEnemyLineup(int round)
    {
        if(round == 1)
        {
            return lineup1;
        }
        else if (round == 2)
        {
            return lineup2;
        }
        else if (round == 3)
        {
            return lineup3;
        }
        else if (round == 4)
        {
            return lineup4;
        }
        else if (round == 5)
        {
            return lineup5;
        }
        else if (round == 6)
        {
            return lineup6;
        }
        else if (round == 7)
        {
            return lineup7;
        }
        else if (round == 8)
        {
            return lineup8;
        }
        else if (round == 9)
        {
            return lineup9;
        }
        else
        {
            return lineup10;
        }
    }
}
