using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for storing card data

[CreateAssetMenu (fileName = "CardData", menuName = "ScriptableObjects/CardData")]
public class CardDataSO : ScriptableObject
{
    public string defaultName;
    public Sprite defaultImage;

    public int defaultHealth;
    public int defaultAttack;
    public int defaultDefense;
    public int defaultSpeed;
    public int defaultTurns;
}
