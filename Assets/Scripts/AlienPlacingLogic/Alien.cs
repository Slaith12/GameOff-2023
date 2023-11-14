using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Alien : MonoBehaviour
{
    private Card card;

    private string cardName { get; set; }
    private Sprite image { get; set; }

    private int attack { get; set; }
    private int defense { get; set; }
    private int speed { get; set; }
    private int turns { get; set; }
    private int health { get; set; }

    private string effect { get; set; }

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image imageIMG;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private TextMeshProUGUI effectText;   

    private void Awake()
    {
        
    }

    private void UpdateVisuals()
    {
        nameText.SetText(cardName);
        imageIMG.sprite = image;

        attackText.SetText(attack.ToString());
        defenseText.SetText(defense.ToString());
        speedText.SetText(speed.ToString());
        turnsText.SetText(turns.ToString());
        healthText.SetText(health.ToString());

        effectText.SetText(effect.ToString());

        // implement visual updates for the rest of the variables
    }

    public void UpdateStats(string cardName, Sprite image, int attack, int defense, int speed, int turns, int health, string effect)
    {
        this.cardName = cardName;
        this.image = image;

        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.turns = turns;
        this.health = health;

        this.effect = effect;

        UpdateVisuals();
    }
}
