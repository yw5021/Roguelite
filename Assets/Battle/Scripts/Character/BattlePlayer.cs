using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BattlePlayer : BattleCharacter
{
    [SerializeField]
    protected GameObject skillAttackPrefab;

    protected List<int> skillList = new List<int>();

    protected bool isBuff = false;

    protected int baseDefense = 0;

    protected float buffDuration = 1f;

    protected override void Awake()
    {
        attackDamage = GameManager.Instance.PlayerAbility.AttackDamage;
        defense = GameManager.Instance.PlayerAbility.Defense;
        attackSpeed = GameManager.Instance.PlayerAbility.AttackSpeed;

        characterMaxHp = GameManager.Instance.PlayerMaxHp;
        characterHp = GameManager.Instance.PlayerNowHp;

        basicAttackRange = 2f;

        baseAttackDamage = 10f;

        basicAttackDelay = 1f;
        basicAttackDuration = 0.5f;
        basicAttackStartDelay = 0.1f;

        moveSpeed = 1.5f;

        attackDelayTime = 0f;

        animator = GetComponent<Animator>();

        foreach (PlayerArtifact artifact in GameManager.Instance.Artifacts)
        {
            ApplyArtifactData(artifact);
        }

        foreach (PlayerSkillBloc skillBloc in GameManager.Instance.EquipedPlayerSkillBlocs)
        {
            if(GameManager.Instance.GetPlayerSkillData(skillBloc.Index).SkillType == SkillType.Passive)
            {
                ApplyPassiveSkill(skillBloc.Index);
            }
            else
            {
                skillList.Add(skillBloc.Index);
            }
        }
    }

    protected override void Update()
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

                GameManager.Instance.SetPlayerHp(characterHp);

                BattleHandler.Instance.BattleEnd(true);
                break;

            case BattleState.Move:
                animator.SetBool("isRun",true);

                MoveToTarget();
                break;

            case BattleState.BasicAttack:
                animator.SetBool("isRun", false);

                AttackStart();
                break;

            case BattleState.UseSkill:
                animator.SetBool("isRun", false);

                UseSkillStart();
                break;
        }
    }

    protected virtual void ApplyArtifactData(PlayerArtifact artifact)
    {
        PlayerArtifactData artifactData = GameManager.Instance.GetPlayerArtifactData(artifact.Index);

        if (artifactData == null)
        {
            Debug.LogError("Error 30369 - ArtifactIndex Error");
            return;
        }

        System.Type type = typeof(BattlePlayer);

        FieldInfo field = type.GetField(artifactData.EffectType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        object value = field.GetValue(this);

        float setValue = (float)value + artifactData.EffectBaseValue + (artifact.Stack * artifactData.EffectStackValue);

        field.SetValue(this, setValue);
    }

    protected override bool isSkillReady()
    {
        if (isBuff)
        {
            return false;
        }

        if(skillList.Count == 0)
        {
            return false;
        }

        if (!isAttackRange())
        {
            return false;
        }

        return true;
    }

    protected virtual void UseSkillStart()
    {
        if (isAttackDelay())
        {
            return;
        }

        StartCoroutine(UseSkill(skillList[0]));

        skillList.RemoveAt(0);
    }

    protected virtual IEnumerator UseSkill(int index)
    {
        switch (index)
        {
            case 1:
                Vector3 pos = new Vector3(attackTarget.transform.position.x, attackTarget.transform.position.y, -1);

                GameObject attackGo = Instantiate(skillAttackPrefab, pos, Quaternion.identity);

                BattleBasicAttack attack = attackGo.GetComponent<BattleBasicAttack>();

                float resultAttackDuration = basicAttackDuration - ((basicAttackDuration * 0.50f) * attackSpeed * 0.01f);
                float resultAttackDamage = baseAttackDamage * (1f + (attackDamage * 0.01f)) * 2f;
                float resultAttackStartDelay = basicAttackStartDelay - ((basicAttackStartDelay * 0.50f) * attackSpeed * 0.01f);
                float resultAttackSpeed = 1f + (attackSpeed * 0.01f);

                animator.Play("Attack");

                attack.StartCoroutine(attack.AttackStart(gameObject, resultAttackDuration, resultAttackDamage, resultAttackStartDelay, resultAttackSpeed));

                attackDelayTime = basicAttackDelay * (1f - (attackSpeed * 0.01f * 0.50f));
                break;

            case 3:
                isBuff = true;

                basicAttackDelay = 1f * 0.3f;
                basicAttackDuration = 0.5f * 0.3f;
                basicAttackStartDelay = 0.1f * 0.3f;

                animator.SetFloat("AttackSpeed", (1f + (attackSpeed * 0.01f)) * 3f);

                yield return new WaitForSeconds(3f * buffDuration);

                isBuff = false;

                basicAttackDelay = 1f;
                basicAttackDuration = 0.5f;
                basicAttackStartDelay = 0.1f;

                animator.SetFloat("AttackSpeed", 1f + (attackSpeed * 0.01f));

                break;
        }
    }

    protected virtual void ApplyPassiveSkill(int index)
    {
        switch (index)
        {
            case 2:
                baseAttackDamage += 2;
                break;
        }
    }

    public override float CalcResultDamage(float damage)
    {
        float resultDamage = (damage - baseDefense) * (1f - (defense * 0.01f * 0.50f));

        return resultDamage;
    }

    protected override void OnDead()
    {
        BattleHandler.Instance.BattleEnd(false);

        base.OnDead();
    }
}
