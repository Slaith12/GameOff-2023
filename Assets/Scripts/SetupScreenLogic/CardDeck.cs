using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    public static CardDeck instance;

    [SerializeField] private TextMeshProUGUI deckCounter;
    [SerializeField] private Button button;
    [SerializeField] private Hand hand;

    [SerializeField] private List<CardDataSO> possibleCards;
    [HideInInspector] public List<CardDataSO> deck;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(delegate { hand.RedrawHand(); });
        button.enabled = true;
        deck = new List<CardDataSO>();
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
    public void CreateDeck()
    {
        Debug.Log("Generating Deck");
        deck.Clear();
        for(int i = 0; i < 30; i++)
        {
            deck.Add(possibleCards[Random.Range(0, possibleCards.Count)]);
        }
        UpdateDeckSize();
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

    public CardDataSO TryDrawCard()
    {
        if(!DataManager.instance.generatedDeck)
        {
            CreateDeck();
            DataManager.instance.generatedDeck = true;
        }
        Debug.Log("Drawing Card");
        if (deck.Count == 0)
        {
            Debug.Log("No card to draw");
            return null;
        }
        else
        {
            int index = (int)(Random.value * deck.Count);
            CardDataSO card = deck[index];
            deck.RemoveAt(index);
            UpdateDeckSize();
            return card;
        }
    }

    public void ReturnCard(CardDataSO card)
    {
        deck.Add(card);
        UpdateDeckSize();
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
