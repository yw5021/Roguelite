using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEnemyData
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Vector2 position;

    public GameObject Enemy
    {
        get
        {
            return enemy;
        }
    }

    public Vector2 Position
    {
        get
        {
            return position;
        }
    }
}

[CreateAssetMenu(fileName = "EnemyTableData", menuName = "Scriptable Object/EnemyTableData", order = int.MaxValue)]
public class BattleEnemyTableData : ScriptableObject
{
    [SerializeField]
    private int index;

    [SerializeField]
    private BattleEnemyData[] battleEnemyDatas;

    public BattleEnemyData[] BattleEnemyDatas
    {
        get
        {
            return battleEnemyDatas;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }
    }
}
