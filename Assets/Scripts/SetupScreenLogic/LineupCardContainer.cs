using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineupCardContainer : MonoBehaviour
{
    private Button button;
    public Alien alien;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject pelletDisplay;

    [SerializeField] private TextMeshProUGUI redText;
    [SerializeField] private TextMeshProUGUI yellowText;
    [SerializeField] private TextMeshProUGUI blueText;

    [SerializeField] private Button redButton;
    [SerializeField] private Button yellowButton;
    [SerializeField] private Button blueButton;

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
    
    public void SetCard(Alien alien)
    {
        this.alien = alien;
        GameObject cardInstance = Instantiate(cardPrefab, this.transform);
        cardInstance.GetComponent<Card>().SetAlien(alien, Card.cardState.lineup);
    }
}
