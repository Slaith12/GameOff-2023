using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupCardContainer : MonoBehaviour
{
    private Button button;
    private Card card;

    void Awake()
    {
        button = GetComponent<Button>();
        // button.onClick.AddListener(delegate { CardDataManager.cardDataManager.TransferCard(this.transform); });
    }

    public void SetFields(Card card)
    {
        this.card = card;
    }
}
