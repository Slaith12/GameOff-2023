using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager cardDataManager { get; private set; }

    [SerializeField] private List<CardDataSO> cardData;

    private void Awake()
    {
         DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Hand.instance.PopulateHand(cardData);
    }
}
