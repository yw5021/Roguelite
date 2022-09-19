using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemImage : MonoBehaviour
{
    private Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void SetItemImage(PlayerArtifactData artifact)
    {
        image.sprite = artifact.Sprite;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { GameManager.Instance.OpenImagePopup(artifact.Sprite, artifact.ItemName, artifact.Descript); });
    }
}
