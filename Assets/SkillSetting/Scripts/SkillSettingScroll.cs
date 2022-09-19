using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSettingScroll : MonoBehaviour
{
    private const int startX = 120;
    private const int startY = -150;
    private const int intervalX = 240;
    private const int intervalY = -250;

    private ScrollRect scrollRect;

    private List<SkillSettingBloc> skillBlocs = new List<SkillSettingBloc>();

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();

        CreateSkillBloc();

        UpdateSkillBloc();
    }

    private void CreateSkillBloc()
    {
        for (int i = 0; i < GameManager.Instance.PlayerSkillBlocs.Count; i++)
        {
            GameObject obj = Instantiate(SkillSettingHandler.Instance.blocPrefab, scrollRect.content.transform);

            int x = startX + (intervalX * (i % 3));
            int y = startY + (intervalY * (i / 3));

            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

            obj.transform.localScale = new Vector2(0.7f, 0.7f);

            SkillSettingBloc skillBloc = obj.GetComponent<SkillSettingBloc>();

            skillBlocs.Add(skillBloc);

            skillBloc.Init();

            skillBloc.SkillBlocCreate(GameManager.Instance.PlayerSkillBlocs[i]);
        }

        int contentHeight = Mathf.Abs(startY * 2 + (intervalY * (GameManager.Instance.PlayerSkillBlocs.Count / 3)));

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, contentHeight);
    }

    private void UpdateSkillBloc()
    {
        for (int i = 0; i < GameManager.Instance.EquipedPlayerSkillBlocs.Count; i++)
        {
            PlayerSkillBloc playerSkillBloc = GameManager.Instance.EquipedPlayerSkillBlocs[i];

            foreach(SkillSettingBloc bloc in skillBlocs)
            {
                if(bloc.PlayerSkillBloc == playerSkillBloc)
                {
                    Vector2 pos = GameManager.Instance.CheckEquipBlocPosition(playerSkillBloc);

                    SkillSettingCell cell = SkillSettingHandler.Instance.SkillSettingTable.SkillTable[(int)pos.x, (int)pos.y];

                    cell.EquipSkillSettingBloc(bloc, true);
                }
            }
        }
    }
}
