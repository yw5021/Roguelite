using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootSelectHandler : MonoBehaviour
{
    private enum RootDifficult
    {
        Easy,
        Normal,
        Hard
    }

    private static RootSelectHandler instance;
    public static RootSelectHandler Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Button[] difficultSelectButtons;

    private void Awake()
    {
        Init();

        ButtonInit();
    }
    private void OnDestroy()
    {
        instance = null;
    }

    private void SettingRoot(RootDifficult difficult)
    {
        switch (difficult)
        {
            case RootDifficult.Easy:
                GameManager.Instance.SetBattleRoot(1);
                GameManager.Instance.SetBattleRoot(2);
                break;

            case RootDifficult.Normal:
                GameManager.Instance.SetBattleRoot(3);
                GameManager.Instance.SetBattleRoot(4);
                break;

            case RootDifficult.Hard:
                GameManager.Instance.SetBattleRoot(5);
                GameManager.Instance.SetBattleRoot(6);
                break;
        }
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("RootSelectHandler");
            if (go == null)
            {
                Debug.LogError("Error 49842 - Not Ready RootSelectHandler");
            }
            if (go.GetComponent<RootSelectHandler>() == null)
            {
                go.AddComponent<RootSelectHandler>();
            }

            instance = go.GetComponent<RootSelectHandler>();
        }
    }

    private void ButtonInit()
    {
        difficultSelectButtons[0].onClick.AddListener(()=> { SettingRoot(RootDifficult.Easy); GameManager.Instance.LoadScene("BattleScene"); });
        difficultSelectButtons[1].onClick.AddListener(()=> { SettingRoot(RootDifficult.Normal); GameManager.Instance.LoadScene("BattleScene"); });
        difficultSelectButtons[2].onClick.AddListener(()=> { SettingRoot(RootDifficult.Hard); GameManager.Instance.LoadScene("BattleScene"); });
    }
}
