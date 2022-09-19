using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestSceneHandler : MonoBehaviour
{
    [SerializeField]
    private Button recoverHpButton;
    [SerializeField]
    private Button shopSceneButton;
    [SerializeField]
    private Button skillSettingSceneButton;
    [SerializeField]
    private Button nextSceneButton;

    private void Awake()
    {
        ButtonInit();
    }

    private void ButtonInit()
    {
        recoverHpButton.onClick.AddListener(RecoverHpButtonEvent);

        shopSceneButton.onClick.AddListener(() => { GameManager.Instance.LoadScene("ShopScene"); });
        skillSettingSceneButton.onClick.AddListener(() => { GameManager.Instance.LoadScene("SkillSettingScene"); });
        nextSceneButton.onClick.AddListener(() => { GameManager.Instance.LoadScene("BattleScene"); });
    }

    private void RecoverHpButtonEvent()
    {
        if (GameManager.Instance.UsePlayerGold(10))
        {
            GameManager.Instance.RecoveryPlayerHp(15);
        }
    }
}
