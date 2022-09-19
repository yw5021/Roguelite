using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBasicAttack : MonoBehaviour
{
    protected GameObject attackCharacter;

    protected bool isReady = false;

    protected float attackDuration;
    protected float time = 0f;
    protected float attackDamage;
    protected float attackSpeed;

    protected bool isTriggerEnd = false;

    public IEnumerator AttackStart(GameObject character, float duration, float damage, float startDelay, float speed)
    {
        yield return new WaitForSeconds(startDelay);

        attackCharacter = character;
        attackDuration = duration;
        attackDamage = damage;
        attackSpeed = speed;

        isReady = true;

        GetComponent<BoxCollider2D>().enabled = true;

        GetComponent<SpriteRenderer>().enabled = false;
    }

    protected void Update()
    {
        if (!isReady)
        {
            return;
        }

        AttackDurationCheck();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        OnDamaged(other.gameObject);
    }

    protected void OnDamaged(GameObject target)
    {
        if (isTriggerEnd)
        {
            return;
        }

        if (target.gameObject == attackCharacter)
        {
            return;
        }

        BattleCharacter character = target.GetComponent<BattleCharacter>();

        float resultDamage = character.CalcResultDamage(attackDamage);

        character.OnDamaged(resultDamage);

        GameObject damageText = Instantiate(BattleHandler.Instance.BattleDamageText, BattleHandler.Instance.Canvas.transform);

        damageText.GetComponent<BattleDamageText>().OnDamaged(target.transform.position, resultDamage);

        isTriggerEnd = true;

        //Debug.Log($"{attackCharacter.name} 가 {target.name} 에게 {attackDamage} 의 데미지");
    }

    protected void AttackDurationCheck()
    {
        if (time >= attackDuration)
        {
            Destroy(gameObject);
        }

        time += Time.deltaTime;
    }
}
