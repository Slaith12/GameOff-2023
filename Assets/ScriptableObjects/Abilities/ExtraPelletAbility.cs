using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Extra Pellet Ability", menuName = "ScriptableObjects/Abilities/Extra Pellet")]
public class ExtraPelletAbility : AbilitySO
{
    public override void OnLineupReinit(int index, Alien alien)
    {
        SetupScreenDataSaver.instance.AddRandomPellet();
    }
}
