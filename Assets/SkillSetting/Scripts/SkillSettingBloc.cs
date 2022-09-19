using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSettingBloc : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const float blocSize = 60f;

    private PlayerSkillBloc playerSkillBloc;


    private Transform parent;

    private RectTransform rect;
    private Vector2 originPosition;

    private SkillSettingCell nowEquipedCell;

    public PlayerSkillBloc PlayerSkillBloc
    {
        get
        {
            return playerSkillBloc;
        }
    }

    public void Init()
    {
        rect = GetComponent<RectTransform>();

        parent = transform.parent;


        originPosition = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (nowEquipedCell != null)
        {
            UnEquipSkillSettingBloc();
        }

        SkillSettingHandler.Instance.nowGrabedBloc = this;

        transform.SetParent(SkillSettingHandler.Instance.canvas.transform);
        transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData data)
    {
        rect.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y - Screen.height);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (SkillSettingHandler.Instance.nowEnterCell == null)
        {
            SkillSettingHandler.Instance.nowGrabedBloc = null;

            ResetPosition();
        }
        else
        {
            SkillSettingHandler.Instance.nowEnterCell.EquipSkillSettingBloc(this, false);

            SkillSettingHandler.Instance.nowEnterCell = null;
        }
    }

    public void SkillBlocCreate(PlayerSkillBloc playerSkillBloc)
    {
        this.playerSkillBloc = playerSkillBloc;

        BlocPosition[] blocPositions = playerSkillBloc.BlocPositions;

        for (int i = 0; i < blocPositions.Length; i++)
        {
            BlocPosition blocPosition = blocPositions[i];

            GameObject bloc = Instantiate(SkillSettingHandler.Instance.blocCellPrefab, transform);

            bloc.GetComponent<RectTransform>().anchoredPosition = new Vector2(blocSize * blocPosition.X, - blocSize * blocPosition.Y);

            SkillType type = GameManager.Instance.GetPlayerSkillData(playerSkillBloc.Index).SkillType;

            switch (type)
            {
                case SkillType.Active:
                    bloc.GetComponent<Image>().color = Color.red;
                    break;

                case SkillType.Passive:
                    bloc.GetComponent<Image>().color = Color.blue;
                    break;

                case SkillType.Buff:
                    bloc.GetComponent<Image>().color = Color.green;
                    break;

            }
        }
    }

    public void ResetPosition()
    {
        transform.SetParent(parent);

        transform.localScale = new Vector2(0.5f, 0.5f);

        rect.anchoredPosition = originPosition;
    }

    public void EquipSkillSettingBloc(SkillSettingCell cell, bool isInit)
    {
        Debug.Log("EquipBloc");

        nowEquipedCell = cell;

        transform.SetParent(cell.transform.parent);

        rect.anchoredPosition = cell.gameObject.GetComponent<RectTransform>().anchoredPosition;

        if (!isInit)
        {
            GameManager.Instance.EquipSkillBloc(playerSkillBloc, new Vector2(nowEquipedCell.X, nowEquipedCell.Y));
        }
        else
        {
            transform.SetParent(cell.transform.parent);
            transform.localScale = Vector3.one;
        }

        SkillSettingHandler.Instance.UpdateEqiupSkillText();
    }

    public void UnEquipSkillSettingBloc()
    {
        Debug.Log("UnEquipBloc");

        nowEquipedCell.UnEquipSkillSettingBloc();

        transform.SetParent(SkillSettingHandler.Instance.canvas.transform);

        nowEquipedCell = null;

        GameManager.Instance.UnEquipSkillBloc(playerSkillBloc);

        SkillSettingHandler.Instance.UpdateEqiupSkillText();
    }
}
