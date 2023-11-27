using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckCounter;
    [SerializeField] private Button button;
    [SerializeField] private Hand hand;

    [SerializeField] private List<CardDataSO> possibleCards;
    public List<CardDataSO> deck;

    private void Awake()
    {
        CreateDeck();

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(delegate { hand.RedrawHand(); });
        button.enabled = true;
    }

    private void CreateDeck()
    {
        for(int i = 0; i < 50; i++)
        {
            deck.Add(possibleCards[Random.Range(0, possibleCards.Count)]);
        }
    }

    public void Reshuffle()
    {
        List<CardDataSO> tempList = new List<CardDataSO>();
        foreach(CardDataSO cardData in deck)
        {
            tempList.Add(cardData);
        }
        deck.Clear();
        int tempListSize = tempList.Count;
        for(int i = 0; i < tempListSize; i++)
        {
            int tempRand = Random.Range(0, tempList.Count);
            deck.Add(tempList[tempRand]);
            tempList.RemoveAt(tempRand);
        }
    }

    public void UpdateDeckSize()
    {
        deckCounter.SetText(deck.Count.ToString());
    }

    public void DisableButton()
    {
        button.enabled = false;
    }
}
