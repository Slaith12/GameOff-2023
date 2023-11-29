using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public virtual void OnLineupPlace(int index, Alien alien) { }
    public virtual void OnLineupRemove(int index, Alien alien) { }

    /// <summary>
    /// <para>This should be used to subscribe to combat-phase events that will be used as triggers for the ability.</para>
    /// <para>This should be called whenever an alien is loaded in the setup phase, including when combat starts and if the alien spawns/respawns mid-combat.</para>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>This should not be used as an "On Combat Start" event, as it is to be called when the alien is spawned as well, even if it's mid-combat.</para>
    /// <para>Instead, you can use this function to subscribe to an "OnCombatStart" or "OnSpawn" event for the alien and use that to trigger an ability.</para>
    /// <para>Any abilities that affect or are triggered by other aliens should be properly reversed/disposed of when the alien is killed.</para>
    /// <para>Any abilities that affect an alien's stats that's not meant to persist between combat rounds should also be reversed.</para>
    /// </remarks>
    /// 
    /// <param name="alien">The alien that owns the ability. The parameter type is meant to be the combat-phase representation of the alien which contains events for the ability to subscribe to.</param>
    public virtual void InitCombat(AlienCombat alien) { }
}
