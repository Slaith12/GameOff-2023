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

    public Alien[] GetEnemyLineup(int round)
    {
        List<CardDataSO> baseLineup;
        switch(round)
        {
            case 1:
                baseLineup = lineup1;
                break;
            case 2:
                baseLineup = lineup2;
                break;
            case 3:
                baseLineup = lineup3;
                break;
            case 4:
                baseLineup = lineup4;
                break;
            case 5:
                baseLineup = lineup5;
                break;
            case 6:
                baseLineup = lineup6;
                break;
            case 7:
                baseLineup = lineup7;
                break;
            case 8:
                baseLineup = lineup8;
                break;
            case 9:
                baseLineup = lineup9;
                break;
            case 10:
                baseLineup = lineup10;
                break;
            default:
                baseLineup = lineup1;
                Debug.LogError("Invalid round entered");
                break;
        }
        Alien[] fullLineup = new Alien[5];
        for(int i = 0; i < Mathf.Min(baseLineup.Count, fullLineup.Length); i++)
        {
            if (baseLineup[i] == null)
                continue;
            fullLineup[i] = new Alien(baseLineup[i]);
        }
        return fullLineup;
    }
}
