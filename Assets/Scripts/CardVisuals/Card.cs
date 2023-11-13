using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDataSO initialCardStats;

    private string cardName;
    private Sprite cardImage;
    private Sprite alienImage;

    private int attack;
    private int defense;
    private int speed;
    private int turns;
    private int health;

    private string effect;

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
        cardName = initialCardStats.defaultName;
        cardImage = initialCardStats.defaultCardImage;
        alienImage = initialCardStats.defaultAlienImage;

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
        button.onClick.AddListener(delegate { CardDataManager.cardDataManager.SetSelectedCard(this); });
    }

    public void SetCardDataSO(CardDataSO initialCardStats)
    {
        this.initialCardStats = initialCardStats;
    }

    public string GetCardName()
    {
        return cardName;
    }
    public Sprite GetCardImage()
    {
        return cardImage;
    }
    public Sprite GetAlienImage()
    {
        return alienImage;
    }
    public int GetAttack()
    {
        return attack;
    }
    public int GetDefense()
    {
        return defense;
    }
    public int GetSpeed()
    {
        return speed;
    }
    public int GetTurns()
    {
        return turns;
    }
    public int GetHealth()
    {
        return health;
    }
    public string GetEffect()
    {
        return effect;
    }
}
