using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienContainer : MonoBehaviour
{
    private Button button;
    private Card card;
    private AlienUI alien;

    void Awake()
    {
        button = GetComponent<Button>();
        // button.onClick.AddListener(delegate { CardDataManager.cardDataManager.TransferCard(this.transform); });
    }

    public void SetFields(Card card, AlienUI alien)
    {
        this.card = card;
        this.alien = alien;
        this.alien.UpdateStats(card.name, card.alienImage, card.attack, card.defense, card.speed, card.turns, card.health, card.effect);
    }
}
