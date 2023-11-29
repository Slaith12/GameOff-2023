using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineupCardContainer : MonoBehaviour
{
    private Button button;
    private Alien m_alien;
    public Alien alien => m_alien;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject pelletDisplay;

    [SerializeField] private TextMeshProUGUI redText;
    [SerializeField] private TextMeshProUGUI yellowText;
    [SerializeField] private TextMeshProUGUI blueText;

    [SerializeField] private Button redButton;
    [SerializeField] private Button yellowButton;
    [SerializeField] private Button blueButton;

    //position-based modifiers, needed to make sure modifiers are always applied to whatever alien is in this slot
    private int attackModifier;
    private int defenseModifier;
    private int speedModifier;

    [SerializeField] private int m_index;
    public int index => m_index;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(this.gameObject); });

        redButton.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(redButton.gameObject.GetComponent<LineupPellets>()); });
        yellowButton.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(yellowButton.gameObject.GetComponent<LineupPellets>()); });
        blueButton.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(blueButton.gameObject.GetComponent<LineupPellets>()); });
    }
    
    private void Update()
    {
        if(alien == null)
        {
            pelletDisplay.SetActive(false);
            return;
        }
        pelletDisplay.SetActive(true);

        redText.SetText(alien.attackPellets.ToString());
        yellowText.SetText(alien.speedPellets.ToString());
        blueText.SetText(alien.defensePellets.ToString());
    }
    
    public void SetCard(Alien alien, bool triggerAbilities = true)
    {
        //reset old alien
        if (m_alien != null)
        {
            m_alien.attack -= attackModifier;
            m_alien.defense -= defenseModifier;
            m_alien.speed -= speedModifier;
            if(triggerAbilities)
                LineupManager.instance.RemoveCard(index, m_alien);
        }

        m_alien = alien;

        if (m_alien != null)
        {
            alien.attack += attackModifier;
            alien.defense += defenseModifier;
            alien.speed += speedModifier;
            GameObject cardInstance = Instantiate(cardPrefab, this.transform);
            cardInstance.GetComponent<Card>().SetAlien(alien, Card.cardState.lineup);
            if (triggerAbilities)
                LineupManager.instance.AddCard(index, alien);
        }
    }

    public void AddAttackModifier(int amount)
    {
        attackModifier += amount;
        if (alien != null)
            alien.attack += amount;
    }

    public void AddDefenseModifier(int amount)
    {
        defenseModifier += amount;
        if (alien != null)
            alien.defense += amount;
    }

    public void AddSpeedModifier(int amount)
    {
        speedModifier += amount;
        if (alien != null)
            alien.speed += amount;
    }
}
