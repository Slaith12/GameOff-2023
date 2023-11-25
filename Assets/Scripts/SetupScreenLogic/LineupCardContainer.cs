using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupCardContainer : MonoBehaviour
{
    private Button button;
    private Card card;
    [SerializeField] private GameObject cardPrefab;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(this.gameObject, 0); });
    }

    public void SetCard(Alien alien)
    {
        GameObject cardInstance = Instantiate(cardPrefab, this.transform);

        cardInstance.GetComponent<Card>().SetAlien(alien, Card.cardState.lineup);
    }
}
