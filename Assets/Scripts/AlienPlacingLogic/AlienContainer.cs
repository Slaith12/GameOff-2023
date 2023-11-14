using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienContainer : MonoBehaviour
{
    private Button button;
    private Card card;
    private Alien alien;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { CardDataManager.cardDataManager.TransferCard(this.transform); });
    }

    public void SetFields(Card card, Alien alien)
    {
        this.card = card;
        this.alien = alien;
        this.alien.UpdateStats(card.GetCardName(), card.GetAlienImage(), card.GetAttack(), card.GetDefense(), card.GetSpeed(), card.GetTurns(), card.GetHealth(), card.GetEffect());
    }
}
