using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSettingHandler : MonoBehaviour
{
    private static SkillSettingHandler instance;
    public static SkillSettingHandler Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject blocPrefab;
    public GameObject blocCellPrefab;

    public SkillSettingBloc nowGrabedBloc = null;

    public SkillSettingCell nowEnterCell = null;

    public Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    public Text equipedSkillText;

    [SerializeField]
    private SkillSettingTable skillSettingTable;

    [SerializeField]
    private SkillSettingScroll skillSettingScroll;

    [SerializeField]
    private Button prevSceneButton;

    public SkillSettingTable SkillSettingTable
    {
        get
        {
            return skillSettingTable;
        }
    }

    private void Awake()
    {
        Init();

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);

        prevSceneButton.onClick.AddListener(GameManager.Instance.LoadPrevScene);

        UpdateEqiupSkillText();
    }

    private void Update()
    {
        CheckEnterSkillSettingCell();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void UpdateEqiupSkillText()
    {
        string text = "";

        Dictionary<int, int> skillIndexCount = new Dictionary<int, int>();

        foreach (PlayerSkillBloc skillBloc in GameManager.Instance.EquipedPlayerSkillBlocs)
        {
            if (!skillIndexCount.ContainsKey(skillBloc.Index))
            {
                skillIndexCount.Add(skillBloc.Index, 1);
            }
            else
            {
                skillIndexCount[skillBloc.Index] += 1;
            }
        }

        for (int i = 0; i < GameManager.Instance.SkillDatas.Length; i++)
        {
            int index = GameManager.Instance.SkillDatas[i].Index;

            if (skillIndexCount.ContainsKey(index))
            {
                PlayerSkillData data = GameManager.Instance.GetPlayerSkillData(index);

                text += $"{data.SkillName} X {skillIndexCount[index]} \n";
            }
        }

        equipedSkillText.text = text;
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("SkillSettingHandler");
            if (go == null)
            {
                Debug.LogError("Error 49843 - Not Ready SkillSettingHandler");
            }
            if (go.GetComponent<SkillSettingHandler>() == null)
            {
                go.AddComponent<SkillSettingHandler>();
            }

            instance = go.GetComponent<SkillSettingHandler>();
        }
    }

    private void CheckEnterSkillSettingCell()
    {
        if (nowGrabedBloc != null)
        {
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            if (results.Count > 1)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.name.Contains("SkillSettingCell"))
                    {
                        nowEnterCell = results[i].gameObject.GetComponent<SkillSettingCell>();
                        break;
                    }
                }
            }
            else
            {
                nowEnterCell = null;
            }
        }
    }
}
