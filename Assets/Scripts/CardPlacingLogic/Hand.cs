using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public static Hand instance { get; private set; }

    private List<Card> handList = new List<Card>();
    [SerializeField] private GameObject cardPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void PopulateHand(List<CardDataSO> cardDeck)
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject tempCard = Instantiate(cardPrefab, this.transform);
            tempCard.GetComponent<Card>().SetCardDataSO(cardDeck[i]);
            handList.Add(tempCard.GetComponent<Card>());
            //tempCard.transform.position = new Vector3((100 * i) - 100, 50, 0);
        }
    }
}
