using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyRat : BattleEnemy
{
    protected override void Awake()
    {
        characterMaxHp = 20f;
        characterHp = 20f;

        basicAttackRange = 1.5f;

        baseAttackDamage = 5f;

        basicAttackDelay = 1f;
        basicAttackDuration = 0.5f;
        basicAttackStartDelay = 0.1f;

        moveSpeed = 2f;

        attackDelayTime = 0f;

        animator = GetComponent<Animator>();

        dropExp = 200;
        dropGold = 200;
        dropSkillBlocChance = 100;

        dropSkillBlocLevelPercentage[0] = 30;
        dropSkillBlocLevelPercentage[1] = 20;
        dropSkillBlocLevelPercentage[2] = 20;
        dropSkillBlocLevelPercentage[3] = 20;

        CheckDropSkillBloc();
    }
}
