using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//consider splitting display logic into a separate class
public class CombatManager : MonoBehaviour
{
    public CombatManager instance { get; private set; }

    [SerializeField] GameObject alienPrefab;

    List<AlienCombat> playerAliens;
    List<AlienCombat> enemyAliens;
    bool started;

    public event AlienCombat.IndividualEvent OnAlienSpawn;

    #region Setup & Spawning

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeLineups();
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
