using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleResultPopup : MonoBehaviour
{
    [SerializeField]
    private Button panel;

    [SerializeField]
    private Text contentText;

    [SerializeField]
    private GameObject[] dropItemImages;

    private UnityAction nowButtonAction;

    private void Awake()
    {
        panel.onClick.AddListener(() => { Destroy(gameObject); });
    }

    public void ChangeButtonEvent(UnityAction action)
    {
        if (nowButtonAction != null)
        {
            panel.onClick.RemoveListener(nowButtonAction);
        }

        nowButtonAction = action;

        panel.onClick.AddListener(action);
    }

    public void OpenBattleResultPopup(int resultExp, int resultGold, List<PlayerSkillBloc> dropSkillBloc)
    {
        contentText.text = $"Exp +{resultExp}\n";
        contentText.text += $"\n";
        contentText.text += $"Gold +{resultGold}";

        if (dropSkillBloc != null)
        {
            for (int i = 0; i < dropSkillBloc.Count; i++)
            {
                PlayerSkillData data = GameManager.Instance.GetPlayerSkillData(dropSkillBloc[i].Index);

                if (data == null)
                {
                    Debug.LogError("Error 48790 - Skill Index Error");
                    return;
                }

                dropItemImages[i].GetComponent<Image>().sprite = data.Sprite;

                dropItemImages[i].GetComponent<Button>().onClick.AddListener(() => { GameManager.Instance.OpenImagePopup(data.Sprite,data.SkillName,data.Descript); });            
            }
        }
    }
}
