using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for storing card data

[CreateAssetMenu (fileName = "CardData", menuName = "ScriptableObjects/CardData")]
public class CardDataSO : ScriptableObject
{
    public string displayName;
    public Sprite cardThumbnail;
    public Sprite defaultCombatSprite;
    public RuntimeAnimatorController animations;
    public Sprite customCardFrame;
    public Sprite customCardCounters;
    public Sprite customThumbnailFrame;

    public int defaultHealth;
    public int defaultAttack;
    public int defaultDefense;
    public int defaultSpeed;
    public int defaultRounds;

    public string description;
    public string effectDescription;
    public AbilitySO[] abilities;

    public Vector2 healthBarOffset = new Vector2(3, 15);
}
