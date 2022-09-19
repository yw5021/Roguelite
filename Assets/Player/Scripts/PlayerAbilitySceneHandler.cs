using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilitySceneHandler : MonoBehaviour
{
    public Text attackDamageValueText;
    public Text attackSpeedValueText;
    public Text defenseValueText;
    public Text abilityPointValueText;

    public Button[] attackDamageButton;
    public Button[] attackSpeedButton;
    public Button[] defenseButton;

    public Button resetButton;

    public Button prevButton;

    private PlayerAbility playerAbility;

    private void Awake()
    {
        playerAbility = GameManager.Instance.PlayerAbility;

        ApplyUIPlayerAbility();

        SetButtonEvent();
    }

    private void ApplyUIPlayerAbility()
    {
        attackDamageValueText.text = playerAbility.AttackDamage.ToString();
        defenseValueText.text = playerAbility.Defense.ToString();
        attackSpeedValueText.text = playerAbility.AttackSpeed.ToString();

        abilityPointValueText.text = playerAbility.AbilityPoint.ToString();
    }

    private void SetButtonEvent()
    {
        int value = 1;

        for(int i = 0; i < attackDamageButton.Length; i++)
        {
            int temp = value;

            attackDamageButton[i].onClick.AddListener(()=> { playerAbility.UseAbilityPoint(Ability.AttackDamage, temp); });
            defenseButton[i].onClick.AddListener(()=> { playerAbility.UseAbilityPoint(Ability.Defense, temp); });
            attackSpeedButton[i].onClick.AddListener(()=> { playerAbility.UseAbilityPoint(Ability.AttackSpeed, temp); });

            attackDamageButton[i].onClick.AddListener(ApplyUIPlayerAbility);
            defenseButton[i].onClick.AddListener(ApplyUIPlayerAbility);
            attackSpeedButton[i].onClick.AddListener(ApplyUIPlayerAbility);

            value *= 10;
        }

        resetButton.onClick.AddListener(playerAbility.AbilityReset);
        resetButton.onClick.AddListener(ApplyUIPlayerAbility);

        prevButton.onClick.AddListener(GameManager.Instance.LoadPrevScene);
    }
}
