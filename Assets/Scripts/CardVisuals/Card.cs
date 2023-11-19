using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDataSO initialCardStats;

    public string cardName;
    public Sprite cardImage;
    public Sprite alienImage;

    public int attack;
    public int defense;
    public int speed;
    public int turns;
    public int health;

    public string effect;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image imageIMG;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private TextMeshProUGUI effectText;

    private Button button;

    private void Start()
    {
        cardName = initialCardStats.unchangingName;
        cardImage = initialCardStats.unchangingCardImage;
        alienImage = initialCardStats.unchangingAlienImage;

        attack = initialCardStats.defaultAttack;
        defense = initialCardStats.defaultDefense;
        speed = initialCardStats.defaultSpeed;
        turns = initialCardStats.defaultTurns;
        health = initialCardStats.defaultHealth;

        effect = initialCardStats.defaultEffect;

        nameText.SetText(cardName);
        imageIMG.sprite = cardImage;

        attackText.SetText(attack.ToString());
        defenseText.SetText(defense.ToString());
        speedText.SetText(speed.ToString());
        turnsText.SetText(turns.ToString());
        healthText.SetText(health.ToString());

        effectText.SetText(effect.ToString());

        button = GetComponent<Button>();
        // button.onClick.AddListener(delegate { CardDataManager.cardDataManager.SetSelectedCard(this); });
    }

    public void SetCardDataSO(CardDataSO initialCardStats)
    {
        this.initialCardStats = initialCardStats;
    }
}
