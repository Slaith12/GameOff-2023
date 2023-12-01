using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }

    public List<CardDataSO> cardDeck;

    public Alien[] playerLineup = new Alien[5];
    public Alien[] enemyLineup = new Alien[5];

    public int attackPellets, defensePellets, speedPellets;

    public int currentStage;
    public int numLosses;

    public SpeedManager.Speed combatSpeed;

    [SerializeField] SpriteRenderer fadeOut;
    private int fadeMode;
    private float fadeTimer;
    private const float FADETIME = 0.5f;
    private string nextScene;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        fadeOut.color = new Color(0, 0, 0, 0);
        fadeMode = 0;
    }

    private void Update()
    {
        switch(fadeMode)
        {
            case 0: //none
                break;
            case 1: //fade out
                fadeTimer += Time.deltaTime;
                fadeOut.color = new Color(0, 0, 0, fadeTimer / FADETIME);
                if(fadeTimer >= FADETIME)
                {
                    SceneManager.LoadScene(nextScene);
                    fadeTimer = FADETIME;
                    fadeMode = 2;
                }
                break;
            case 2: //fade in
                fadeTimer -= Time.deltaTime;
                fadeOut.color = new Color(0, 0, 0, fadeTimer / FADETIME);
                if (fadeTimer <= 0)
                    fadeMode = 0;
                break;
        }
    }

    public void GoToCombat()
    {
        fadeTimer = 0;
        nextScene = "Combat";
        fadeMode = 1;
    }

    public void GoToSetup()
    {
        fadeTimer = 0;
        nextScene = "JacobTestScene";
        fadeMode = 1;
    }
}
