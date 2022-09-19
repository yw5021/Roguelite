using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterHpbar : MonoBehaviour
{
    private BattleCharacter parent;

    private float scaleX = 1f;
    private float scaleY = 0.1f;

    private void Awake()
    {
        parent = transform.parent.GetComponent<BattleCharacter>();

        scaleX /= parent.transform.localScale.x;
        scaleY /= parent.transform.localScale.y;

        transform.localScale = new Vector3(scaleX, scaleY, 0f);

        parent.onDamagedEvent += OnDamaged;

        if(parent.GetType() == typeof(BattlePlayer))
        {
            float hpBarRate = (GameManager.Instance.PlayerNowHp / GameManager.Instance.PlayerMaxHp) * scaleX;

            transform.localScale = new Vector3(hpBarRate, scaleY, 0f);
        }
    }

    private void OnDamaged(float damage, float characterHp, float characterMaxHp)
    {
        float hpBarRate = (characterHp / characterMaxHp) * scaleX;

        transform.localScale = new Vector3(hpBarRate, scaleY, 0f);
    }
}
