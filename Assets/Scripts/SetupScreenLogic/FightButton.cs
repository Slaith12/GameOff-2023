using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour
{
    private Button button;

    [SerializeField] private List<EnemyLineupObj> totalLineup = new List<EnemyLineupObj>();

    [SerializeField] private CardDeck deck;

    [SerializeField] private StoredPellets redPel;
    [SerializeField] private StoredPellets yellowPel;
    [SerializeField] private StoredPellets bluePel;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { FightPrep(); });
    }

    private void FightPrep()
    {
        for(int i = 0; i < 5; i++)
        {
            DataManager.instance.playerLineup[i] = ButtonEnabler.instance.lineupCardContainers[i].alien;
        }

        DataManager.instance.enemyLineup[0] = new Alien(totalLineup[DataManager.instance.currentStage].alien1);
        DataManager.instance.enemyLineup[1] = new Alien(totalLineup[DataManager.instance.currentStage].alien2);
        DataManager.instance.enemyLineup[2] = new Alien(totalLineup[DataManager.instance.currentStage].alien3);
        DataManager.instance.enemyLineup[3] = new Alien(totalLineup[DataManager.instance.currentStage].alien4);
        DataManager.instance.enemyLineup[4] = new Alien(totalLineup[DataManager.instance.currentStage].alien5);

        DataManager.instance.cardDeck = deck.deck;

        DataManager.instance.attackPellets = redPel.GetNumPellets();
        DataManager.instance.speedPellets = yellowPel.GetNumPellets();
        DataManager.instance.defensePellets = bluePel.GetNumPellets();

        SceneManager.LoadScene("Combat");
    }
}
