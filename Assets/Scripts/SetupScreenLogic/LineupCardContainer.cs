using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupCardContainer : MonoBehaviour
{
    private Button button;
    private Alien alien;
    [SerializeField] private GameObject cardPrefab;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(this.gameObject); });
    }

    public void SetCard(Alien alien)
    {
        this.alien = alien;
        GameObject cardInstance = Instantiate(cardPrefab, this.transform);
        cardInstance.GetComponent<Card>().SetAlien(alien, Card.cardState.lineup);
    }
}
