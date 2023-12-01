using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoutButton : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private List<GameObject> enemyCardContainers;
    [SerializeField] private GameObject enemyLineupContainer;
    [SerializeField] private TextMeshProUGUI text;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ChangeButton(); } );
    }

    private void Start()
    {
        LineupManager.instance.OnStartAnimations += DisableButton;
        LineupManager.instance.OnEndAnimations += EnableButton;

        Invoke("DelayedStart", 0.01f);
    }

    private void DisableButton(object sender, EventArgs e)
    {
        button.enabled = false;
    }

    private void EnableButton(object sender, EventArgs e)
    {
        button.enabled = true;
    }

    private void DelayedStart()
    {
        for (int i = 0; i < DataManager.instance.enemyLineup.Length; i++)
        {
            if (DataManager.instance.enemyLineup[i] != null)
            {
                GameObject card = Instantiate(cardPrefab, enemyCardContainers[i].transform);
                card.GetComponent<Card>().SetAlien(DataManager.instance.enemyLineup[i], Card.cardState.scout);
                card.GetComponent<Button>().enabled = false;
            }
        }
        text.SetText("Round: " + DataManager.instance.currentStage);
        enemyLineupContainer.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && button.enabled == true)
            ChangeButton();
        if (Input.GetKeyDown(KeyCode.LeftShift) && button.enabled == true)
            ChangeButton();
    }

    private void ChangeButton()
    {
        enemyLineupContainer.SetActive(!enemyLineupContainer.activeInHierarchy);
    }
}
