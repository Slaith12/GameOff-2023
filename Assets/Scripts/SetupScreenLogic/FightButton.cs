using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private CardDeck deck;

    [SerializeField] private StoredPellets redPel;
    [SerializeField] private StoredPellets yellowPel;
    [SerializeField] private StoredPellets bluePel;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { FightPrep(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FightPrep();
        }
    }

    private void FightPrep()
    {
        for (int i = 0; i < 5; i++)
        {
            DataManager.instance.playerLineup[i] = ButtonEnabler.instance.lineupCardContainers[i].alien;
        }

        DataManager.instance.enemyLineup = EnemyProgression.instance.GetEnemyLineup(DataManager.instance.currentStage);

        DataManager.instance.cardDeck = deck.deck;

        DataManager.instance.attackPellets = redPel.GetNumPellets();
        DataManager.instance.speedPellets = yellowPel.GetNumPellets();
        DataManager.instance.defensePellets = bluePel.GetNumPellets();

        DataManager.instance.GoToCombat();
    }
}