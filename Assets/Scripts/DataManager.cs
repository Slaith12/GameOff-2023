using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    public List<CardDataSO> cardDeck;

    public Alien[] playerLineup = new Alien[5];
    public Alien[] enemyLineup = new Alien[5];

    public int attackPellets, defensePellets, speedPellets;

    public int currentStage;
    public int numLosses;

    public SpeedManager.Speed combatSpeed;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
