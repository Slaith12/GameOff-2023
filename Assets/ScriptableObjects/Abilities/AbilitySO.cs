using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    /// <summary>
    /// <para>This should be used to subscribe to setup-phase events that will be used as triggers for the ability.</para>
    /// <para>This should be called whenever an alien is loaded in the setup phase, including when it is placed and when the scene is loaded with the alien in the lineup.</para>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>This should not be used as an "On Alien Placed" event, as it is to be called every time the scene is loaded as well, even after the alien is already placed.</para>
    /// <para>Instead, you can use this function to subscribe to an "OnAlienPlaced" event for the alien and use that to trigger an ability.</para>
    /// <para>Any abilities that affect or are triggered by other aliens should be properly reversed/disposed of when the alien leaves the lineup.</para>
    /// </remarks>
    /// 
    /// <param name="alien">The alien that owns the ability. The parameter type is meant to be the setup-phase representation of the alien which contains events for the ability to subscribe to.</param>
    public virtual void InitSetup(AlienContainer alien) { }

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
