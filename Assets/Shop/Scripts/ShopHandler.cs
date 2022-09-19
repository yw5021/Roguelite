using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    private const int RerollPrice = 10;

    [SerializeField]
    private ShopItemImage[] itemImages;
    [SerializeField]
    private Text[] priceTexts;
    [SerializeField]
    private ShopBuyButton[] buyButtons;
    [SerializeField]
    private Text goldText;
    [SerializeField]
    private Button rerollButton;

    [SerializeField]
    private Button prevSceneButton;

    private void Start()
    {
        ApplyUIGoldText();

        ShopReroll(0);

        rerollButton.onClick.AddListener(() => { ShopReroll(RerollPrice); });

        prevSceneButton.onClick.AddListener(GameManager.Instance.LoadPrevScene);
    }

    private void ShopReroll(int price)
    {
        if (!GameManager.Instance.UsePlayerGold(price))
        {
            return;
        }

        PlayerArtifactData[] artifacts = GameManager.Instance.ArtifactDatas;

        for(int i= 0; i < 3; i++)
        {
            int RandomInt = Random.Range(0, artifacts.Length);
            PlayerArtifactData artifact = artifacts[RandomInt];

            itemImages[i].SetItemImage(artifact);

            priceTexts[i].text = $"Price : {artifact.Price}";

            int temp = i;
            buyButtons[i].ChangeButtonEvent(() => { BuyButtonEvent(buyButtons[temp], artifact); });
            buyButtons[i].ButtonInteractableToggle(true);
        }

        ApplyUIGoldText();
    }

    private void BuyButtonEvent(ShopBuyButton button, PlayerArtifactData artifact)
    {
        if (GameManager.Instance.UsePlayerGold(artifact.Price))
        {
            GameManager.Instance.GetArtifact(new PlayerArtifact(artifact.Index));

            button.ButtonInteractableToggle(false);

            ApplyUIGoldText();
        }
    }

    private void ApplyUIGoldText()
    {
        goldText.text = "Gold : " + GameManager.Instance.PlayerGold;
    }
}
