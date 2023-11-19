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
}
