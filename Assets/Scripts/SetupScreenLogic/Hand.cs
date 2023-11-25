using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardDeck deck;
    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        PopulateHand();
    }

    public void PopulateHand()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetAlien(new Alien(deck.deck[i]), Card.cardState.hand);
            deck.deck.RemoveAt(i);
            ButtonEnabler.instance.handCards.Add(tempCard.GetComponent<Card>());
            deck.UpdateDeckSize();
        }
    }

    public void RedrawHand()
    {
        int handSize = ButtonEnabler.instance.handCards.Count;
        foreach(Card card in ButtonEnabler.instance.handCards)
        {
            deck.deck.Add(card.alien.cardDataSO);
            Destroy(card.gameObject);
        }
        deck.Reshuffle();

        ButtonEnabler.instance.handCards.RemoveRange(0, handSize);

        for(int i = 0; i < handSize; i++)
        {
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetAlien(new Alien(deck.deck[i]), Card.cardState.hand);
            deck.deck.RemoveAt(i);
            ButtonEnabler.instance.handCards.Add(tempCard.GetComponent<Card>());
            deck.UpdateDeckSize();
        }
        deck.DisableButton();
    }
}
