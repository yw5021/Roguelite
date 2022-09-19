using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : BattleCharacter
{
    protected int dropExp;
    protected int dropGold;
    protected PlayerSkillBloc dropSkillBloc;

    protected int dropSkillBlocChance = 100;
    protected int[] dropSkillBlocLevelPercentage = new int[PlayerSkillBloc.maxBlocLevel];

    protected override void Awake()
    {
        characterMaxHp = 30f;
        characterHp = 30f;

        basicAttackRange = 1.5f;

        baseAttackDamage = 3f;

        basicAttackDelay = 1f;
        basicAttackDuration = 0.5f;
        basicAttackStartDelay = 0.1f;

        moveSpeed = 1f;

        attackDelayTime = 0f;

        animator = GetComponent<Animator>();

        dropExp = 200;
        dropGold = 100;
        dropSkillBlocChance = 100;

        dropSkillBlocLevelPercentage[0] = 30;
        dropSkillBlocLevelPercentage[1] = 20;
        dropSkillBlocLevelPercentage[2] = 20;
        dropSkillBlocLevelPercentage[3] = 20;

        CheckDropSkillBloc();
    }

    protected virtual void CheckDropSkillBloc()
    {
        int isDrop = Random.Range(0, 100);

        if (isDrop < dropSkillBlocChance)
        {
            int checkSkillBlocLevel = Random.Range(0, 100);

            int skillBlocLevel = 1;
            for (int i = 0; i < dropSkillBlocLevelPercentage.Length; i++)
            {
                if (checkSkillBlocLevel < dropSkillBlocLevelPercentage[i])
                {
                    skillBlocLevel += i;
                    break;
                }
                else
                {
                    checkSkillBlocLevel -= dropSkillBlocLevelPercentage[i];
                }
            }

            Debug.Log("Level" + skillBlocLevel + " skill Drop");

            dropSkillBloc = new PlayerSkillBloc(skillBlocLevel);
        }
    }

    protected override void FindTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    protected override void OnDead()
    {
        BattleHandler.Instance.AddBattleResultExp(dropExp);
        BattleHandler.Instance.AddBattleResultGold(dropGold);

        if (dropSkillBloc != null)
        {
            BattleHandler.Instance.AddBattleResultSkillBloc(dropSkillBloc);
        }

        base.OnDead();
    }
}
