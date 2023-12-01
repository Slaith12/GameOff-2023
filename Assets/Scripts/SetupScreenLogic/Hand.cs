using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardDeck deck;
    [SerializeField] private GameObject cardPrefab;

    public void PopulateHand()
    {
        for(int i = 0; i < 3; i++)
        {
            CardDataSO newAlien = deck.TryDrawCard();
            if (newAlien == null)
                break;
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetAlien(new Alien(newAlien), Card.cardState.hand);
            ButtonEnabler.instance.handCards.Add(tempCard.GetComponent<Card>());
        }
    }

    public void RedrawHand()
    {
        int handSize = ButtonEnabler.instance.handCards.Count;

        ReturnHand();

        for (int i = 0; i < handSize; i++)
        {
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetAlien(new Alien(deck.deck[i]), Card.cardState.hand);
            deck.deck.RemoveAt(i);
            ButtonEnabler.instance.handCards.Add(tempCard.GetComponent<Card>());
            deck.UpdateDeckSize();
        }
        deck.DisableButton();
        ButtonEnabler.instance.DeleteSelectedItem();
    }

    public void ReturnHand()
    {
        foreach (Card card in ButtonEnabler.instance.handCards)
        {
            deck.ReturnCard(card.alien.cardDataSO);
            Destroy(card.gameObject);
        }

        ButtonEnabler.instance.handCards.Clear();
    }
}
