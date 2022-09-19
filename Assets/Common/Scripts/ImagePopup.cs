using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopup : MonoBehaviour
{
    [SerializeField]
    private Button panel;

    [SerializeField]
    private Image popupImage;

    [SerializeField]
    private Text titleText;

    [SerializeField]
    private Text contentText;

    private void Awake()
    {
        panel.onClick.AddListener(() => { Destroy(gameObject); });
    }

    public void OpenImagePopup(Sprite sprite, string title, string content)
    {
        popupImage.sprite = sprite;
        titleText.text = title;
        contentText.text = content;
    }
}
