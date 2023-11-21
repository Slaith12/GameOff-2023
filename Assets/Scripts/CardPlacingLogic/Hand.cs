using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    private List<Card> handList = new List<Card>();
    [SerializeField] private GameObject cardPrefab;

    public void PopulateHand(List<CardDataSO> cardDeck)
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetAlien(new Alien(cardDeck[i]));
            handList.Add(tempCard.GetComponent<Card>());
        }
    }
}
