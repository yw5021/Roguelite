using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActiveSkill : MonoBehaviour
{
    public int index;

    public bool isReady = false;

    public virtual IEnumerator UseSkill()
    {
        return null;
    }
}
