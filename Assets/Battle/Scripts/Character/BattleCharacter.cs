using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public enum BattleState
    {
        None,

        Wait,
        Move,
        BasicAttack,
        UseSkill,
        Dead
    }

    protected float attackDamage = 0f;
    protected float defense = 0f;
    protected float attackSpeed = 0f;


    protected BattleState nowBattleState = BattleState.Wait;

    protected float characterMaxHp;
    protected float characterHp;

    public delegate void OnDamagedEvent(float damage, float characterHp, float characterMaxHp);
    public event OnDamagedEvent onDamagedEvent;

    protected float baseAttackDamage;

    [SerializeField]
    protected GameObject basicAttackPrefab;

    protected float basicAttackRange;

    protected float basicAttackDelay;
    protected float basicAttackDuration;
    protected float basicAttackStartDelay;

    protected float moveSpeed;


    protected float attackDelayTime;

    protected GameObject[] targets;
    protected GameObject attackTarget;

    protected int[] skillBlocIndexArr;

    protected Animator animator;

    protected virtual void Awake()
    {
        characterMaxHp = 50f;
        characterHp = 50f;

        basicAttackRange = 2f;

        baseAttackDamage = 10f;

        basicAttackDelay = 1f;
        basicAttackDuration = 0.5f;
        basicAttackStartDelay = 0.1f;

        moveSpeed = 1f;

        attackDelayTime = 0f;

        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        CreateHpBar();

        BattleStart();
    }

    protected virtual void Update()
    {
        if (!BattleHandler.Instance.isBattleProgress)
        {
            return;
        }

        CheckBattleState();

        CheckAttackDelay();

        switch (nowBattleState)
        {
            case BattleState.Wait:
                animator.SetBool("isRun", false);
                break;

            case BattleState.Move:
                animator.SetBool("isRun", false);

                MoveToTarget();
                break;

            case BattleState.BasicAttack:
                animator.SetBool("isRun", false);

                AttackStart();
                break;

            case BattleState.UseSkill:
                animator.SetBool("isRun", false);
                break;
        }
    }

    protected virtual void CreateHpBar()
    {
        Instantiate(BattleHandler.Instance.BattleHpBar, transform);
    }

    protected virtual void BattleStart()
    {
        FindTargets();
    }

    protected virtual void CheckBattleState()
    {
        FindTargets();

        SearchAttackTarget();

        if(characterHp <= 0)
        {
            nowBattleState = BattleState.Dead;
        }

        if (targets.Length <= 0)
        {
            nowBattleState = BattleState.Wait;
            return;
        }

        if (isSkillReady())
        {
            nowBattleState = BattleState.UseSkill;
        }
        else if (isAttackRange())
        {
            nowBattleState = BattleState.BasicAttack;
        }
        else
        {
            nowBattleState = BattleState.Move;
        }
    }

    protected virtual void CheckAttackDelay()
    {
        if (attackDelayTime > 0)
        {
            attackDelayTime -= Time.deltaTime;
        }
    }

    protected virtual void FindTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }

    protected virtual void SearchAttackTarget()
    {
        if (targets.Length <= 0)
        {
            Debug.Log("Àû ¾øÀ½");
            return;
        }

        GameObject target = targets[0];
        float min = Vector2.SqrMagnitude(target.transform.position - transform.position);
        for (int i = 1; i < targets.Length; i++)
        {
            GameObject enemy = targets[i];

            Vector2 distance = enemy.transform.position - transform.position;

            float sqr = Vector2.SqrMagnitude(distance);

            if (sqr < min)
            {
                target = enemy;
                min = sqr;
            }
        }

        attackTarget = target;

        Debug.Log("ÇöÀç Å¸°Ù - " + attackTarget.name);
    }

    protected virtual void MoveToTarget()
    {
        Vector3 dir = attackTarget.transform.position - transform.position;

        transform.Translate((dir.normalized * moveSpeed * Time.deltaTime));
    }

    protected virtual bool isAttackRange()
    {
        float distance = Vector2.Distance(transform.position, attackTarget.transform.position);

        //Debug.Log($"{gameObject.name} : distance - {distance}, position - {transform.position}, target - {attackTarget.transform.position}");

        if (distance <= basicAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual bool isAttackDelay()
    {
        if (attackDelayTime > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual bool isSkillReady()
    {
        //test
        return false;
    }

    protected virtual void AttackStart()
    {
        if (isAttackDelay())
        {
            return;
        }

        Attack(attackTarget);
    }

    protected virtual void Attack(GameObject target)
    {
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, -1);

        GameObject attackGo = Instantiate(basicAttackPrefab, pos, Quaternion.identity);

        BattleBasicAttack attack = attackGo.GetComponent<BattleBasicAttack>();

        float resultAttackDuration = basicAttackDuration - ((basicAttackDuration * 0.50f) * attackSpeed * 0.01f);
        float resultAttackDamage = baseAttackDamage * (1f + (attackDamage * 0.01f));
        float resultAttackStartDelay = basicAttackStartDelay - ((basicAttackStartDelay * 0.50f) * attackSpeed * 0.01f);
        float resultAttackSpeed = 1f + (attackSpeed * 0.01f);

        animator.SetFloat("AttackSpeed", resultAttackSpeed);

        animator.Play("Attack");

        attack.StartCoroutine(attack.AttackStart(gameObject, resultAttackDuration, resultAttackDamage, resultAttackStartDelay, resultAttackSpeed));

        attackDelayTime = basicAttackDelay * (1f - (attackSpeed * 0.01f * 0.50f));
    }

    public virtual float CalcResultDamage(float damage)
    {
        float resultDamage = damage * (1f - (defense * 0.01f * 0.50f));

        return resultDamage;
    }

    public virtual void OnDamaged(float damage)
    {
        characterHp -= damage;

        if(onDamagedEvent != null)
        {
            onDamagedEvent(damage, characterHp, characterMaxHp);
        }

        isDead();
    }

    protected virtual void isDead()
    {
        if(characterHp <= 0)
        {
            animator.Play("Death");

            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        Debug.Log($"{gameObject.name} »ç¸Á");

        Destroy(gameObject);
    }
}
