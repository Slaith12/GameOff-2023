using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager cardDataManager { get; private set; }

    [SerializeField] private List<CardDataSO> cardData;
    [SerializeField] private GameObject alienPrefab;

    private Card selectedCard;

    private void Awake()
    {
        cardDataManager = this;
         DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Hand.instance.PopulateHand(cardData);
    }

    public void SetSelectedCard(Card selectedCard)
    {
        this.selectedCard = selectedCard;
    }

    public void TransferCard(Transform container)
    {
        if(selectedCard != null)
        {
            Alien tempAlien = Instantiate(alienPrefab, container).GetComponent<Alien>();
            container.GetComponent<AlienContainer>().SetFields(selectedCard, tempAlien);
            Destroy(selectedCard.gameObject);
        }
    }
}
