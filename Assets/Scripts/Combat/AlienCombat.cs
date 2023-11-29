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
    public List<DOTInfo> currentDOTs;

    Vector3 targetPos;
    float interpTime;

    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] HealthBar healthBar;
    Animator animator;

    public delegate void IndividualEvent(AlienCombat alien);
    //Event Order: BeforeAttack, BeforeDamage, AfterDamage, Death, AfterAttack
    public delegate void CombatEvent(AlienCombat attacker, AlienCombat defender, ref int damage);
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
        currentDOTs = new List<DOTInfo>();

        transform.position = (player ? Vector2.left : Vector2.right) * (7 + 10*index);
        transform.localScale = new Vector3(player ? -1 : 1, 1, 1); //all alien sprites face left by default, player aliens should face right
        renderer.sprite = alienData.cardDataSO.defaultCombatSprite;
        animator.runtimeAnimatorController = alienData.cardDataSO.animations;

        healthBar.transform.localPosition = alienData.cardDataSO.healthBarOffset;
        healthBar.transform.localScale = transform.localScale; //if the root object is flipped, re-flip the health bar so it faces the right way
        healthBar.SetHealth(alien.health, alien.health);

        foreach(AbilitySO ability in alien.cardDataSO.abilities)
        {
            ability.InitCombat(this);
        }
    }

    /// <summary>
    /// <para>THIS FUNCTION SHOULD ONLY BE USED BY COMBATMANAGER!!</para>
    /// <para>If any other script needs to change an alien's position, use one of CombatManager's functions</para>
    /// See: <see cref="CombatManager.ShiftPlayerAlien(AlienCombat, int)"/>, <see cref="CombatManager.ShiftEnemyAlien(AlienCombat, int)"/>
    /// </summary>
    public void ChangeIndex(int newIndex, float time = 1.25f)
    {
        index = newIndex;
        ShiftToPos((player ? Vector2.left : Vector2.right) * (7 + 10 * index), time);
    }

    public void ShiftToPos(Vector2 pos, float time = 1.25f)
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
        //can't be a foreach loop because it causes an error if I remove an element from the list in the middle of the loop
        for(int i = 0; i < currentDOTs.Count; i++)
        {
            DOTInfo dot = currentDOTs[i];
            dot.tickTimer += Time.deltaTime;
            //don't take DOT damage when dead, will reactivate OnDamage and OnDeath events improperly
            if(dot.tickTimer >= dot.tickDelay && damageTaken < alienData.health)
            {
                dot.tickTimer -= dot.tickDelay;
                dot.remainingTicks--;
                DamageNonAttacker(dot.damagePerTick);
                if(dot.remainingTicks <= 0)
                {
                    currentDOTs.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void ApplyDOT(DOTInfo newDOT)
    {
        if(currentDOTs.Find(existingDOT => existingDOT.source == newDOT.source) != null)
        {
            return;
        }
        currentDOTs.Add(newDOT);
    }

    //parameters would be used for displaying damage numbers, not currently implemented
    public void UpdateHealthBar(int recentDamage, bool displayDamage = true)
    {
        healthBar.SetHealth(alienData.health - damageTaken, alienData.health);
    }

    public void Attack(AlienCombat defender)
    {
        animator.SetTrigger("Attack");
        int damage = (this.alienData.attack + 10 - defender.alienData.defense) * 4;
        OnBeforeAttack?.Invoke(this, defender, ref damage);
        defender.Damage(this, ref damage);
        OnAfterAttack?.Invoke(this, defender, ref damage);
    }

    public void Damage(AlienCombat attacker, ref int damage)
    {
        int prevDamage = damageTaken;
        OnBeforeDamage?.Invoke(attacker, this, ref damage);

        damageTaken += damage;
        UpdateHealthBar(recentDamage: damage);

        OnAfterDamage?.Invoke(attacker, this, ref damage);
        if (damageTaken >= alienData.health)
            Death();
    }

    public void DamageNonAttacker(int damage, bool triggerEvents = true, bool displayDamage = true)
    {
        if (triggerEvents)
            OnBeforeDamage?.Invoke(null, this, ref damage);

        damageTaken += damage;
        UpdateHealthBar(recentDamage: damage, displayDamage);

        if (triggerEvents)
            OnAfterDamage?.Invoke(null, this, ref damage);
        if (damageTaken >= alienData.health)
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
