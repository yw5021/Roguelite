using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    private static BattleHandler instance;
    public static BattleHandler Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject battleHpBar;
    [SerializeField]
    private GameObject battleDamageText;
    [SerializeField]
    private GameObject battleResultPopup;

    public bool isBattleProgress = false;

    private int battleResultGold = 0;
    private int battleResultExp = 0;
    private List<PlayerSkillBloc> battleResultSkillBloc = new List<PlayerSkillBloc>();

    public GameObject Canvas
    {
        get
        {
            return canvas;
        }
    }

    public GameObject BattleHpBar
    {
        get
        {
            return battleHpBar;
        }
    }

    public GameObject BattleDamageText
    {
        get
        {
            return battleDamageText;
        }
    }

    private void Awake()
    {
        Init();

        int index = GameManager.Instance.NextBattleTableIndex;

        BattleEnemyTableData table = GameManager.Instance.GetBattleEnemyTableData(index);

        BattleStart(table);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("BattleHandler");
            if (go == null)
            {
                Debug.LogError("Error 49840 - Not Ready BattleHandler");
            }
            if (go.GetComponent<BattleHandler>() == null)
            {
                go.AddComponent<BattleHandler>();
            }

            instance = go.GetComponent<BattleHandler>();
        }
    }

    public void BattleStart(BattleEnemyTableData enemyTable)
    {
        Debug.Log("Battle Start");

        isBattleProgress = true;

        foreach (BattleEnemyData enemyData in enemyTable.BattleEnemyDatas)
        {
            Instantiate(enemyData.Enemy, enemyData.Position, Quaternion.identity);
        }
    }

    public void BattleEnd(bool isWin)
    {
        Debug.Log("Battle end");

        isBattleProgress = false;

        if (isWin)
        {
            BattleResultPopup popup = Instantiate(battleResultPopup).GetComponent<BattleResultPopup>();

            popup.OpenBattleResultPopup(battleResultExp, battleResultGold, battleResultSkillBloc);

            GameManager.Instance.GainPlayerExp(battleResultExp);
            GameManager.Instance.GainPlayerGold(battleResultGold);

            foreach (PlayerSkillBloc skillBloc in battleResultSkillBloc) 
            {
                GameManager.Instance.GetSkillBloc(skillBloc);
            }

            UnityEngine.Events.UnityAction action = () => { GameManager.Instance.LoadScene("RestScene"); };

            if (GameManager.Instance.BattleRootLength > 0)
            {
                action = () => { GameManager.Instance.LoadScene("RestScene"); };
            }
            else
            {
                GameManager.Instance.RecoveryPlayerHp(GameManager.Instance.PlayerMaxHp);
                action = () => { GameManager.Instance.LoadScene("MainScene"); };
            }

            popup.ChangeButtonEvent(action);
        }
        else
        {
            GameManager.Instance.RecoveryPlayerHp(GameManager.Instance.PlayerMaxHp);
            GameManager.Instance.LoadScene("MainScene");
        }
    }

    public void AddBattleResultGold(int gold)
    {
        battleResultGold += gold;
    }

    public void AddBattleResultExp(int exp)
    {
        battleResultExp += exp;
    }

    public void AddBattleResultSkillBloc(PlayerSkillBloc skillBloc)
    {
        battleResultSkillBloc.Add(skillBloc);
    }
}
