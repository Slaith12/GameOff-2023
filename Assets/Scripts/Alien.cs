using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien
{
    public CardDataSO cardDataSO { get; set; }

    public int attack { get; set; }
    public int defense { get; set; }
    public int speed { get; set; }
    public int rounds { get; set; }
    public int health { get; set; }

    public int attackPellets { get; set; }
    public int defensePellets { get; set; }
    public int speedPellets { get; set; }

    public Alien()
    {

    }

    public Alien(CardDataSO cardDataSO)
    {
        this.cardDataSO = cardDataSO;

        attack = cardDataSO.defaultAttack;
        defense = cardDataSO.defaultDefense;
        speed = cardDataSO.defaultSpeed;
        rounds = cardDataSO.defaultRounds;
        health = cardDataSO.defaultHealth;

        attackPellets = 0;
        defensePellets = 0;
        speedPellets = 0;
    }

    public Alien(CardDataSO cardDataSO, int attackPellets, int defensePellets, int speedPellets)
    {
        this.cardDataSO = cardDataSO;

        this.attackPellets = attackPellets;
        this.defensePellets = defensePellets;
        this.speedPellets = speedPellets;

        attack = cardDataSO.defaultAttack + attackPellets;
        defense = cardDataSO.defaultDefense + defensePellets;
        speed = cardDataSO.defaultSpeed + speedPellets;
        rounds = cardDataSO.defaultRounds;
        health = cardDataSO.defaultHealth;
    }

    public void ChangeAttackPellets(int amount)
    {
        attackPellets += amount;
        attack += amount;
    }

    public void ChangeDefensePellets(int amount)
    {
        defensePellets += amount;
        defense += amount;
    }

    public void ChangeSpeedPellets(int amount)
    {
        speedPellets += amount;
        speed += amount;
    }
}
