using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopBuyButton : MonoBehaviour
{
    private Button button;

    private UnityAction nowButtonAction;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void ChangeButtonEvent(UnityAction action)
    {
        if (nowButtonAction != null)
        {
            button.onClick.RemoveListener(nowButtonAction);
        }

        nowButtonAction = action;

        button.onClick.AddListener(action);
    }

    public void ButtonInteractableToggle(bool toggle)
    {
        button.interactable = toggle;
    }
}
