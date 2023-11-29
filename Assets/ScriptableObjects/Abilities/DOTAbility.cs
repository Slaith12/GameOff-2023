using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DOTType { Fire, Poison }

public class DOTInfo
{
    public AlienCombat source; //so that the same alien can't stack DOTs
    public DOTType type;
    public int damagePerTick;
    public int remainingTicks;
    public float tickDelay;
    public float tickTimer;

    public DOTInfo(AlienCombat source, DOTType type, int damagePerTick, int numTicks, float duration)
    {
        this.source = source;
        this.type = type;
        this.damagePerTick = damagePerTick;
        remainingTicks = numTicks;
        tickDelay = duration / numTicks;
        tickTimer = 0;
    }
}

[CreateAssetMenu(fileName = "DOT Ability", menuName = "ScriptableObjects/Abilities/DOT")]
public class DOTAbility : AbilitySO
{
    [SerializeField] DOTType type;
    [SerializeField] int damagePerTick;
    [SerializeField] int numTicks;
    [SerializeField] float duration;

    public override void InitCombat(AlienCombat alien)
    {
        base.InitCombat(alien);
        alien.OnAfterAttack += ApplyDOT;
    }

    private void ApplyDOT(AlienCombat attacker, AlienCombat defender, ref int _)
    {
        DOTInfo dot = new DOTInfo(attacker, type, damagePerTick, numTicks, duration);
        defender.ApplyDOT(dot);
    }
}
