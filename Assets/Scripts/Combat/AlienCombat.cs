using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCombat : MonoBehaviour
{
    public Alien alienData;

    //used to be able to find alien in CombatManager
    public bool player;
    public int index;

    public int damageTaken;

    Vector3 targetPos;
    float interpTime;

    [SerializeField] new SpriteRenderer renderer;
    Animator animator;

    public delegate void IndividualEvent(AlienCombat alien);
    //Event Order: BeforeAttack, BeforeDamage, AfterDamage, Death, AfterAttack
    public delegate void CombatEvent(AlienCombat attacker, AlienCombat defender);
    /// <summary>
    /// Called when this alien is about to attack. Can be used to modify upcoming damage or do similar actions.
    /// </summary>
    public event CombatEvent OnBeforeAttack;
    /// <summary>
    /// Called when this alien is about to be damaged. Can be used modify incoming damage or do similar actions.
    /// </summary>
    public event CombatEvent OnBeforeDamage;
    /// <summary>
    /// Called when this alien has just been damaged. Can be used to react to damage or do similar actions.
    /// </summary>
    public event CombatEvent OnAfterDamage;
    /// <summary>
    /// Called when this alien has just attacked. Can be used to apply additional effects after damage or do similar actions.
    /// </summary>
    public event CombatEvent OnAfterAttack;
    /// <summary>
    /// Called when this alien has died. Can be used to react to death or do similar actions.
    /// </summary>
    public event IndividualEvent OnDeath;

    public void Initialize(Alien alien, int index, bool player)
    {
        alienData = alien;
        this.index = index;
        this.player = player;
        animator = GetComponent<Animator>();

        transform.position = (player ? Vector2.left : Vector2.right) * (7 + 10*index);
        transform.localScale = new Vector3(player ? -1 : 1, 1, 1); //all alien sprites face left by default, player aliens should face right
        renderer.sprite = alienData.cardDataSO.unchangingAlienImage;
        animator.runtimeAnimatorController = alienData.cardDataSO.animations;
    }

    /// <summary>
    /// <para>THIS FUNCTION SHOULD ONLY BE USED BY COMBATMANAGER!!</para>
    /// <para>If any other script needs to change an alien's position, use one of CombatManager's functions</para>
    /// See: <see cref="CombatManager.ShiftPlayerAlien(AlienCombat, int)"/>, <see cref="CombatManager.ShiftEnemyAlien(AlienCombat, int)"/>
    /// </summary>
    public void ChangeIndex(int newIndex, float time = 0.5f)
    {
        index = newIndex;
        ShiftToPos((player ? Vector2.left : Vector2.right) * (7 + 10 * index), time);
    }

    public void ShiftToPos(Vector2 pos, float time = 0.2f)
    {
        targetPos = pos;
        interpTime = time;
    }

    private void Update()
    {
        if(interpTime > 0)
        {
            float interpAmount = Time.deltaTime / interpTime;
            if (interpAmount > 1)
                interpAmount = 1;
            Vector3 interpVector = targetPos - transform.position;
            transform.position += interpVector * Mathf.Sqrt(interpAmount);
            interpTime -= Time.deltaTime;
        }
    }

    public void Attack(AlienCombat defender)
    {
        animator.SetTrigger("Attack");
        OnBeforeAttack?.Invoke(this, defender);
        defender.Damage(attacker: this);
        OnAfterAttack?.Invoke(this, defender);
    }

    public void Damage(AlienCombat attacker)
    {
        OnBeforeDamage?.Invoke(attacker, this);
        damageTaken += (attacker.alienData.attack + 10 - this.alienData.defense) * 4;
        OnAfterDamage?.Invoke(attacker, this);
        if (damageTaken > alienData.health)
            Death();
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        OnDeath?.Invoke(this);
    }

    //Animation Event
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
