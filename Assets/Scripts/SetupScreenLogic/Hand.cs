using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    private List<Card> handList = new List<Card>();

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
            handList.Add(tempCard.GetComponent<Card>());
        }
    }
}
