using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDataSO initialCardStats;

    private string cardName;
    private Sprite image;

    private int attack;
    private int defense;
    private int speed;
    private int turns;
    private int health;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image imageIMG;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        cardName = initialCardStats.defaultName;
        image = initialCardStats.defaultImage;

        attack = initialCardStats.defaultAttack;
        defense = initialCardStats.defaultDefense;
        speed = initialCardStats.defaultSpeed;
        turns = initialCardStats.defaultTurns;
        health = initialCardStats.defaultHealth;

        nameText.SetText(cardName);
        imageIMG.sprite = image;

        attackText.SetText(attack.ToString());
        defenseText.SetText(defense.ToString());
        speedText.SetText(speed.ToString());
        turnsText.SetText(turns.ToString());
        healthText.SetText(health.ToString());
    }

    public void SetCardDataSO(CardDataSO initialCardStats)
    {
        this.initialCardStats = initialCardStats;
    }
}
