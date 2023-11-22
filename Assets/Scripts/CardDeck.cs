using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckCounter;

    [SerializeField] private List<CardDataSO> possibleCards;
    public List<CardDataSO> deck;

    private void Awake()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {
        for(int i = 0; i < 50; i++)
        {
            deck.Add(possibleCards[Random.Range(0, possibleCards.Count)]);
        }
    }

    private void Update()
    {
        deckCounter.SetText(deck.Count.ToString());
    }
}
