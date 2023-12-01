using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private CardDeck deck;
    [SerializeField] private Hand hand;

    [SerializeField] private StoredPellets redPel;
    [SerializeField] private StoredPellets yellowPel;
    [SerializeField] private StoredPellets bluePel;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { FightPrep(); });
    }

    private void Start()
    {
        LineupManager.instance.OnStartAnimations += DisableButton;
        LineupManager.instance.OnEndAnimations += EnableButton;
    }

    private void DisableButton(object sender, EventArgs e)
    {
        button.enabled = false;
    }

    private void EnableButton(object sender, EventArgs e)
    {
        button.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && button.enabled == true)
        {
            FightPrep();
        }
    }

    private void FightPrep()
    {
        hand.ReturnHand();

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