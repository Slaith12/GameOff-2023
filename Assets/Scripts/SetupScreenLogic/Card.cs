using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour, ISelectable
{
    public Alien alien;

    public enum cardState
    {
        hand,
        lineup,
    }
    public cardState currentCardState;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image imageIMG;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private TextMeshProUGUI descriptionText;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(this); });
    }

    public void SetAlien(Alien alien, cardState currentCardState)
    {
        this.alien = alien;
        this.currentCardState = currentCardState;

        nameText.SetText(alien.cardDataSO.displayName);
        imageIMG.sprite = alien.cardDataSO.cardThumbnail;

        attackText.SetText(alien.cardDataSO.defaultAttack.ToString());
        defenseText.SetText(alien.cardDataSO.defaultDefense.ToString());
        speedText.SetText(alien.cardDataSO.defaultSpeed.ToString());
        turnsText.SetText(alien.cardDataSO.defaultRounds.ToString());
        healthText.SetText(alien.cardDataSO.defaultHealth.ToString());

        descriptionText.SetText(alien.cardDataSO.description);
    }

    private void Update()
    {
        nameText.SetText(alien.cardDataSO.displayName);
        imageIMG.sprite = alien.cardDataSO.cardThumbnail;

        attackText.SetText(alien.attack.ToString());
        defenseText.SetText(alien.defense.ToString());
        speedText.SetText(alien.speed.ToString());
        turnsText.SetText(alien.rounds.ToString());
        healthText.SetText(alien.health.ToString());

        descriptionText.SetText(alien.cardDataSO.description);
    }

    public int GetSelectableType()
    {
        return 0;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Transform GetParentTransform()
    {
        return transform.parent;
    }
}
