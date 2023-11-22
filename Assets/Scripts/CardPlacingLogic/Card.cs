using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Alien alien;

    public enum cardState
    {
        hand,
        lineup,
    }

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
        nameText.SetText(alien.cardDataSO.unchangingName);
        imageIMG.sprite = alien.cardDataSO.unchangingCardImage;

        attackText.SetText(alien.cardDataSO.defaultAttack.ToString());
        defenseText.SetText(alien.cardDataSO.defaultDefense.ToString());
        speedText.SetText(alien.cardDataSO.defaultSpeed.ToString());
        turnsText.SetText(alien.cardDataSO.defaultRounds.ToString());
        healthText.SetText(alien.cardDataSO.defaultHealth.ToString());

        descriptionText.SetText(alien.cardDataSO.unchangingDescription);

        button = GetComponent<Button>();
        //button.onClick.AddListener(delegate { CardDataManager.cardDataManager.SetSelectedCard(this); });
    }

    public void SetAlien(Alien alien)
    {
        this.alien = alien;
    }
}
