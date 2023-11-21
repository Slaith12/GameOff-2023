using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/CardPlacingLogic/Card.cs
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
=======
    public Alien alien;
>>>>>>> Stashed changes:Assets/Scripts/CardVisuals/Card.cs

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image imageIMG;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private TextMeshProUGUI descriptionText;

    private Button button;

    private void Start()
    {
<<<<<<< Updated upstream:Assets/Scripts/CardPlacingLogic/Card.cs
        cardName = initialCardStats.defaultName;
        cardImage = initialCardStats.defaultCardImage;
        alienImage = initialCardStats.defaultAlienImage;
=======
>>>>>>> Stashed changes:Assets/Scripts/CardVisuals/Card.cs

        nameText.SetText(alien.cardDataSO.unchangingName);
        imageIMG.sprite = alien.cardDataSO.unchangingCardImage;

<<<<<<< Updated upstream:Assets/Scripts/CardPlacingLogic/Card.cs
        effect = initialCardStats.defaultEffect;
=======
        attackText.SetText(alien.cardDataSO.defaultAttack.ToString());
        defenseText.SetText(alien.cardDataSO.defaultDefense.ToString());
        speedText.SetText(alien.cardDataSO.defaultSpeed.ToString());
        turnsText.SetText(alien.cardDataSO.defaultRounds.ToString());
        healthText.SetText(alien.cardDataSO.defaultHealth.ToString());
>>>>>>> Stashed changes:Assets/Scripts/CardVisuals/Card.cs

        descriptionText.SetText(alien.cardDataSO.unchangingDescription);

        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { CardDataManager.cardDataManager.SetSelectedCard(this); });
    }

    public void SetAlien(Alien alien)
    {
        this.alien = alien;
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
