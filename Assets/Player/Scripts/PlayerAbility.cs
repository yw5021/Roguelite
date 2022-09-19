using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    AttackDamage = 1,
    Defense = 2,
    AttackSpeed = 3
}

public class PlayerAbility
{
    public const int maxAbility = 100;

    private int attackDamage;
    private int defense;
    private int attackSpeed;

    private int abilityPoint;

    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
    }

    public int Defense
    {
        get
        {
            return defense;
        }
    }

    public int AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
    }

    public int AbilityPoint
    {
        get
        {
            return abilityPoint;
        }
    }

    public PlayerAbility(int attackDamage,int defense, int attackSpeed, int abilityPoint)
    {
        this.attackDamage = attackDamage;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        this.abilityPoint = abilityPoint;
    }

    public void UseAbilityPoint(Ability ability, int point)
    {
        int resultUsePoint = point;

        if (abilityPoint < point)
        {
            resultUsePoint = abilityPoint;
        }

        switch (ability)
        {
            case Ability.AttackDamage:
                if(attackDamage == maxAbility)
                {
                    return;
                }
                else if(attackDamage + resultUsePoint > maxAbility)
                {
                    resultUsePoint = maxAbility - attackDamage;
                }

                attackDamage += resultUsePoint;

                break;

            case Ability.Defense:
                if (defense == maxAbility)
                {
                    return;
                }
                else if (defense + resultUsePoint > maxAbility)
                {
                    resultUsePoint = maxAbility - defense;
                }

                defense += resultUsePoint;

                break;

            case Ability.AttackSpeed:
                if (attackSpeed == maxAbility)
                {
                    return;
                }
                else if (attackSpeed + resultUsePoint > maxAbility)
                {
                    resultUsePoint = maxAbility - attackSpeed;
                }

                attackSpeed += resultUsePoint;

                break;
        }

        abilityPoint -= resultUsePoint;
    }

    public void AbilityReset()
    {
        abilityPoint += attackDamage + defense + attackSpeed;

        attackDamage = 0;
        defense = 0;
        attackSpeed = 0;
    }
}
