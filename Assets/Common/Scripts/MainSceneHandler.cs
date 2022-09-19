using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneHandler : MonoBehaviour
{
    [SerializeField]
    private Text playerLevelText;

    [SerializeField]
    private Button abilitySceneButton;

    [SerializeField]
    private Button gameStartSceneButton;

    [SerializeField]
    private Button shopSceneButton;

    [SerializeField]
    private Button skillSettingSceneButton;

    private void Awake()
    {
        playerLevelText.text = "Player Level - " + GameManager.Instance.PlayerLevel;

        abilitySceneButton.onClick.AddListener(()=> { GameManager.Instance.LoadScene("PlayerAbilityScene"); });
        gameStartSceneButton.onClick.AddListener(()=> { GameManager.Instance.LoadScene("RootSelectScene"); });
        shopSceneButton.onClick.AddListener(()=> { GameManager.Instance.LoadScene("ShopScene"); });
        skillSettingSceneButton.onClick.AddListener(()=> { GameManager.Instance.LoadScene("SkillSettingScene"); });
    }
}
