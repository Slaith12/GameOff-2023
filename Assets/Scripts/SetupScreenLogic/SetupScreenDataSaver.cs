using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScreenDataSaver : MonoBehaviour
{
    public static SetupScreenDataSaver instance { get; private set; }

    [SerializeField] private CardDeck deck;

    [SerializeField] private StoredPellets redPel;
    [SerializeField] private StoredPellets yellowPel;
    [SerializeField] private StoredPellets bluePel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DataManager.instance.enemyLineup[0] = new Alien(EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage)[0]);
        DataManager.instance.enemyLineup[1] = new Alien(EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage)[1]);
        DataManager.instance.enemyLineup[2] = new Alien(EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage)[2]);
        DataManager.instance.enemyLineup[3] = new Alien(EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage)[3]);
        DataManager.instance.enemyLineup[4] = new Alien(EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage)[4]);

        LineupManager.instance.Initialize(DataManager.instance.playerLineup);

        if (DataManager.instance.currentStage == 1 & DataManager.instance.numLosses == 0) return;

        deck.deck = DataManager.instance.cardDeck;

        AddRandomPellet();

        redPel.SetNumPellets(DataManager.instance.attackPellets);
        yellowPel.SetNumPellets(DataManager.instance.speedPellets);
        bluePel.SetNumPellets(DataManager.instance.defensePellets);
    }

    public void AddRandomPellet()
    {
        switch((int)(Random.value*3))
        {
            case 0:
                redPel.ChangeNumPellets(1);
                break;
            case 1:
                yellowPel.ChangeNumPellets(1);
                break;
            case 2:
                bluePel.ChangeNumPellets(1);
                break;
        }
    }
}
