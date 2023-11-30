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

    //position-based modifiers, needed to make sure modifiers are always applied to whatever alien is in this slot
    private int attackModifier;
    private int defenseModifier;
    private int speedModifier;

    [SerializeField] private int m_index;
    public int index => m_index;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(gameObject); });
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

    public void AddAttackModifier(int amount, bool applyToCurrent = true)
    {
        attackModifier += amount;
        if (alien != null && applyToCurrent)
            alien.attack += amount;
    }

    public void AddDefenseModifier(int amount, bool applyToCurrent = true)
    {
        defenseModifier += amount;
        if (alien != null && applyToCurrent)
            alien.defense += amount;
    }

    public void AddSpeedModifier(int amount, bool applyToCurrent = true)
    {
        speedModifier += amount;
        if (alien != null && applyToCurrent)
            alien.speed += amount;
    }

    //DO NOT use this function. SetCard() should be used to set the alien in this slot as the function also includes checks to update the card and trigger/reset abilities appropiately
    //Only use this function if you're 100% sure that SetCard() will not work for what you need and that you'll be able to deal with the side effects yourself
    //public void SetAlien(Alien alien)
    //{
    //    m_alien = alien;
    //}
}
