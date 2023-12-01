using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupScreenDataSaver : MonoBehaviour
{
    public static SetupScreenDataSaver instance { get; private set; }

    [SerializeField] private CardDeck deck;
    [SerializeField] private Hand hand;
    [SerializeField] TMP_Text roundText;
    [SerializeField] TMP_Text endText;

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

        roundText.text = $"Round {DataManager.instance.currentStage}/10";

        if (DataManager.instance.currentStage != 1 || DataManager.instance.numLosses != 0)
        {
            LineupManager.instance.Initialize(DataManager.instance.playerLineup);

            deck.deck = DataManager.instance.cardDeck;

            AddRandomPellet();

            bool lineupActive = false;

            for (int i = 0; i < DataManager.instance.playerLineup.Length; i++)
            {
                if(DataManager.instance.playerLineup[i] != null)
                {
                    if (DataManager.instance.playerLineup[i].rounds == 1)
                    {
                        DataManager.instance.playerLineup[i] = null;
                    }
                    else
                    {
                        lineupActive = true;
                    }
                }
            }

            if (DataManager.instance.currentStage > 10)
            {
                endText.text = "You've defeated all the rounds!\nYou Win!";
                endText.transform.parent.gameObject.SetActive(true);
            }
            else if (DataManager.instance.generatedDeck && DataManager.instance.cardDeck.Count == 0 && !lineupActive)
            {
                endText.text = "You've run out of cards.\nYou Lose.";
                endText.transform.parent.gameObject.SetActive(true);
            }
        }

        hand.PopulateHand();
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
