using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//consider splitting display logic into a separate class
public class CombatManager : MonoBehaviour
{
    const string SETUP_SCENE = "JacobTestScene";

    public static CombatManager instance { get; private set; }

    //how long a 1 speed creature would take to attack.
    //the time it takes for a creature to attack is this value/speed.
    public const float attackTimeThreshold = 10f;
    //the percentage of the time threshold already filled when an alien gets to the front
    //higher values benefit slower creatures more
    public const float startingTimeBoost = 0.25f;

    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject alienPrefab;

    List<AlienCombat> playerAliens;
    public int numPlayerAliens => playerAliens.Count;
    public float playerAttackTime;
    List<AlienCombat> enemyAliens;
    public int numEnemyAliens => enemyAliens.Count;
    public float enemyAttackTime;
    bool started;

    public event AlienCombat.IndividualEvent OnAlienSpawn;
    public event AlienCombat.IndividualEvent OnPlayerAttackerChange;
    public event AlienCombat.IndividualEvent OnEnemyAttackerChange;

    #region Setup & Spawning

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);

        InitializeLineups();

        playerAttackTime = attackTimeThreshold*startingTimeBoost;
        OnPlayerAttackerChange += delegate { playerAttackTime = attackTimeThreshold * startingTimeBoost; };
        enemyAttackTime = attackTimeThreshold * startingTimeBoost;
        OnEnemyAttackerChange += delegate { enemyAttackTime = attackTimeThreshold * startingTimeBoost; };

        started = true;
    }

    void InitializeLineups()
    {
        if (DataManager.instance == null || DataManager.instance.playerLineup == null || DataManager.instance.enemyLineup == null)
        {
            Debug.LogError("DataManager not initialized correctly. Either instance is missing or doesn't contain lineups.");
            return;
        }

        playerAliens = new List<AlienCombat>();
        foreach (Alien alien in DataManager.instance.playerLineup)
        {
            if (alien == null)
                continue;
            AddPlayerAlien(alien);
        }

        enemyAliens = new List<AlienCombat>();
        foreach (Alien alien in DataManager.instance.enemyLineup)
        {
            if (alien == null)
                continue;
            AddEnemyAlien(alien);
        }
    }

    public AlienCombat AddPlayerAlien(Alien alien, int index = -1)
    {
        if(index > playerAliens.Count || index < 0)
        {
            index = playerAliens.Count;
        }

        AlienCombat newAlien = Instantiate(alienPrefab).GetComponent<AlienCombat>();
        newAlien.Initialize(alien, index, true);
        playerAliens.Insert(index, newAlien);

        //push back aliens behind new alien (if applicable)
        for (int i = numPlayerAliens - 1; i > index; i--)
        {
            //only play an animation if alien is spawned after initialization
            if (!started)
            {
                playerAliens[i].ChangeIndex(i, 0);
            }
            else
            {
                playerAliens[i].ChangeIndex(i);
            }
        }
        OnAlienSpawn?.Invoke(newAlien);
        return newAlien;
    }

    public AlienCombat AddEnemyAlien(Alien alien, int index = -1)
    {
        if (index > enemyAliens.Count || index < 0)
        {
            index = enemyAliens.Count;
        }

        AlienCombat newAlien = Instantiate(alienPrefab).GetComponent<AlienCombat>();
        newAlien.Initialize(alien, index, false);
        enemyAliens.Insert(index, newAlien);

        //push back aliens behind new alien (if applicable)
        for (int i = numEnemyAliens - 1; i > index; i--)
        {
            //only play an animation if alien is spawned after initialization
            if (!started)
            {
                enemyAliens[i].ChangeIndex(i, 0);
            }
            else
            {
                enemyAliens[i].ChangeIndex(i);
            }
        }
        OnAlienSpawn?.Invoke(newAlien);
        return newAlien;
    }

    #endregion

    private void Update()
    {
        if(playerAliens.Count == 0)
        {
            Lose();
            Debug.Log("You Lose!");
            enabled = false;
            return;
        }
        if(enemyAliens.Count == 0)
        {
            Win();
            Debug.Log("You Win!");
            enabled = false;
            return;
        }
        playerAttackTime += Time.deltaTime * playerAliens[0].alienData.speed;
        enemyAttackTime += Time.deltaTime * enemyAliens[0].alienData.speed;
        if (playerAttackTime >= attackTimeThreshold)
        {
            playerAttackTime -= attackTimeThreshold;
            playerAliens[0].Attack(enemyAliens[0]);
        }
        if (enemyAttackTime >= attackTimeThreshold)
        {
            enemyAttackTime -= attackTimeThreshold;
            enemyAliens[0].Attack(playerAliens[0]);
        }
    }

    void Win()
    {
        DataManager.instance.currentStage++;
        winUI.SetActive(true);
    }

    void Lose()
    {
        DataManager.instance.numLosses++;
        loseUI.SetActive(true);
    }

    public void GoToSetup()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SETUP_SCENE);
    }

    public void ShiftPlayerAlien(AlienCombat alien, int newIndex)
    {
        Debug.LogError("ShiftPlayerAlien not implemented yet because I'm lazy. Make it yourself.");
        //if you want to implement this, it's basically the same code as RemovePlayerAlien with a few modifications.
    }

    public void ShiftEnemyAlien(AlienCombat alien, int newIndex)
    {
        Debug.LogError("ShiftEnemyAlien not implemented yet because I'm lazy. Make it yourself.");
        //if you want to implement this, it's basically the same code as RemoveEnemyAlien with a few modifications.
    }

    public void RemovePlayerAlien(AlienCombat alien)
    {
        if(!playerAliens.Contains(alien))
        {
            Debug.LogError("RemovePlayerAlien attempted to remove alien not in player list.");
            return;
        }
        for(int i = alien.index + 1; i < playerAliens.Count; i++)
        {
            playerAliens[i].ChangeIndex(i - 1);
        }
        playerAliens.RemoveAt(alien.index);
        if (alien.index == 0)
            OnPlayerAttackerChange?.Invoke(alien);
    }

    public void RemoveEnemyAlien(AlienCombat alien)
    {
        if (!enemyAliens.Contains(alien))
        {
            Debug.LogError("RemoveEnemyAlien attempted to remove alien not in enemy list.");
            return;
        }
        for (int i = alien.index + 1; i < enemyAliens.Count; i++)
        {
            enemyAliens[i].ChangeIndex(i - 1);
        }
        enemyAliens.RemoveAt(alien.index);
        if (alien.index == 0)
            OnEnemyAttackerChange?.Invoke(alien);
    }

    public void CombatStepDebug(int id)
    {
        switch(id)
        {
            case 1:
                playerAliens[0].Attack(enemyAliens[0]);
                break;
            case 2:
                enemyAliens[0].Attack(playerAliens[0]);
                break;
        }
    }
}
