using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for storing card data

[CreateAssetMenu (fileName = "CardData", menuName = "ScriptableObjects/CardData")]
public class CardDataSO : ScriptableObject
{
    public string unchangingName;
    public Sprite unchangingCardImage;
    public Sprite unchangingAlienImage;

    public int defaultHealth;
    public int defaultAttack;
    public int defaultDefense;
    public int defaultSpeed;
    public int defaultRounds;

    public string unchangingDescription;
    public string unchangingEffect;
}
