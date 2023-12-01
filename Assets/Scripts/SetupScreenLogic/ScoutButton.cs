using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutButton : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private List<GameObject> enemyCardContainers;
    [SerializeField] private GameObject enemyLineupContainer;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ChangeButton(); } );
    }

    void Start()
    {
        Invoke("DelayedStart", 0.01f);
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
        enemyLineupContainer.SetActive(false);
    }

    private void ChangeButton()
    {
        enemyLineupContainer.SetActive(!enemyLineupContainer.activeInHierarchy);
        button.enabled = false;
        button.enabled = true;
    }
}
