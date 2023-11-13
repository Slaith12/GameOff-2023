using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for storing card data

[CreateAssetMenu (fileName = "CardData", menuName = "ScriptableObjects/CardData")]
public class CardDataSO : ScriptableObject
{
    public string defaultName;
    public Sprite defaultCardImage;
    public Sprite defaultAlienImage;

    public int defaultHealth;
    public int defaultAttack;
    public int defaultDefense;
    public int defaultSpeed;
    public int defaultTurns;

    public string defaultEffect;
}
