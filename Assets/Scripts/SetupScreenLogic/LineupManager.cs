using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineupManager : MonoBehaviour
{
    public static LineupManager instance { get; private set; }

    public delegate void LineupAction(int index, Alien alien);

    public LineupCardContainer[] lineupCards;

    public event LineupAction OnCardAdded;
    public event LineupAction OnCardRemoved;

    private void Awake()
    {
        instance = this;
    }

    //called from LineupCardContainer AFTER card is added, invokes events
    public void AddCard(int index, Alien alien)
    {
        foreach(AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.OnLineupPlace(index, alien);
        }
        OnCardAdded?.Invoke(index, alien);
    }

    //called from LineupCardContainer AFTER card is removed, invokes events
    public void RemoveCard(int index, Alien alien)
    {
        foreach (AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.OnLineupRemove(index, alien);
        }
        OnCardRemoved?.Invoke(index, alien);
    }
}
