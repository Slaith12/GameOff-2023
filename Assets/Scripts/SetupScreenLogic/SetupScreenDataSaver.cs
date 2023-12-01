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
        DataManager.instance.enemyLineup = EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage);

        redPel.SetNumPellets(DataManager.instance.attackPellets);
        yellowPel.SetNumPellets(DataManager.instance.speedPellets);
        bluePel.SetNumPellets(DataManager.instance.defensePellets);

        if (DataManager.instance.currentStage == 1 & DataManager.instance.numLosses == 0) return;

        LineupManager.instance.Initialize(DataManager.instance.playerLineup);

        deck.deck = DataManager.instance.cardDeck;

        AddRandomPellet();
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
