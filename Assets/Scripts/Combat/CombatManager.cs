using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//consider splitting display logic into a separate class
public class CombatManager : MonoBehaviour
{
    public CombatManager instance { get; private set; }

    //how long a 1 speed creature would take to attack.
    //the time it takes for a creature to attack is this value/speed.
    const float attackTimeThreshold = 4f;
    //the percentage of the time threshold already filled when an alien gets to the front
    //higher values benefit slower creatures more
    const float startingTimeBoost = 0.25f;

    [SerializeField] GameObject alienPrefab;

    List<AlienCombat> playerAliens;
    public float playerAttackTime;
    List<AlienCombat> enemyAliens;
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

        //push back aliens behind new alien (if applicable)
        for(int i = index; i < playerAliens.Count; i++)
        {
            //only play an animation if alien is spawned after initialization
            if (!started)
            {
                playerAliens[i].ChangeIndex(i + 1, 0);
            }
            else
            {
                playerAliens[i].ChangeIndex(i + 1);
            }
        }
        playerAliens.Add(newAlien);
        newAlien.OnDeath += RemovePlayerAlien;
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

        //push back aliens behind new alien (if applicable)
        for (int i = index; i < enemyAliens.Count; i++)
        {
            //only play an animation if alien is spawned after initialization
            if (!started)
            {
                enemyAliens[i].ChangeIndex(i+1, 0);
            }
            else
            {
                enemyAliens[i].ChangeIndex(i + 1);
            }
        }
        enemyAliens.Add(newAlien);
        newAlien.OnDeath += RemoveEnemyAlien;
        OnAlienSpawn?.Invoke(newAlien);
        return newAlien;
    }

    #endregion

    private void Update()
    {
        if(playerAliens.Count == 0)
        {
            //lose code
            Debug.Log("You Lose!");
            enabled = false;
            return;
        }
        if(enemyAliens.Count == 0)
        {
            //win code
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
            Debug.LogError("RemoveEnemyAlien attempted to remove alien not in player list.");
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
