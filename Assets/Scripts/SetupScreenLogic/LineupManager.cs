using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineupManager : MonoBehaviour
{
    public static LineupManager instance { get; private set; }

    public delegate void LineupAction(int index, Alien alien);

    public LineupCardContainer[] lineupCards;

    public event LineupAction OnCardAdded;
    public event LineupAction OnCardRemoved;

    [SerializeField] private StoredPellets redPel;
    [SerializeField] private StoredPellets bluePel;
    [SerializeField] private StoredPellets yellowPel;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(Alien[] lineup = null)
    {
        if (lineup == null)
            lineup = new Alien[0];

        for(int i = 0; i < Mathf.Min(lineup.Length, lineupCards.Length); i++)
        {
            if (lineup[i] == null)
                continue;
            lineupCards[i].SetCard(lineup[i], triggerAbilities: false);
        }

        //cards should be re-initialized AFTER all cards are placed in the lineup,
        //since placing cards can cause some extra effects even with triggerAbilities false
        foreach(LineupCardContainer card in lineupCards)
        {
            if(card.alien != null)
                ReInitCard(card.index, card.alien);
        }

        //if you want to add code to have aliens lose rounds, put it here or anywhere after the initialization function
        foreach (LineupCardContainer cardContainer in lineupCards)
        {
            if (cardContainer.alien == null)
                continue;
            cardContainer.alien.rounds -= 1;
            if (cardContainer.alien.rounds == 0)
            {
                cardContainer.SetCard(null);
                redPel.ChangeNumPellets(cardContainer.alien.attackPellets);
                bluePel.ChangeNumPellets(cardContainer.alien.defensePellets);
                yellowPel.ChangeNumPellets(cardContainer.alien.speedPellets);
                Destroy(cardContainer.GetComponentInChildren<Card>().gameObject);
            }
            else 
                cardContainer.SetCard(cardContainer.alien, triggerAbilities: false);
        }
    }

    //called from LineupCardContainer AFTER card is added, invokes events
    public void AddCard(int index, Alien alien)
    {
        foreach(AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.OnLineupPlace(index, alien);
        }
        OnCardAdded?.Invoke(index, alien);
    }

    //called from LineupCardContainer AFTER card is removed, invokes events
    public void RemoveCard(int index, Alien alien)
    {
        foreach (AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.OnLineupRemove(index, alien);
        }
        OnCardRemoved?.Invoke(index, alien);
    }

    //called from LineupCardContainer AFTER card is added, invokes events
    //meant for cards already in lineup when scene is loaded
    public void ReInitCard(int index, Alien alien)
    {
        foreach(AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.OnLineupReinit(index, alien);
        }
    }
}
