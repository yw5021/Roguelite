using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSettingCell : MonoBehaviour
{
    public bool isEquiped = false;

    private int x;
    private int y;

    private SkillSettingTable skillSettingTable;

    private SkillSettingBloc nowEquipBloc;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public void Init(SkillSettingTable skillSettingTable, int x, int y)
    {
        this.skillSettingTable = skillSettingTable;

        this.x = x;
        this.y = y;
    }

    public void EquipSkillSettingBloc(SkillSettingBloc bloc, bool isInit)
    {
        nowEquipBloc = bloc;

        skillSettingTable.CheckEquipSkillSettingBloc(bloc, this, isInit);
    }

    public void UnEquipSkillSettingBloc()
    {
        skillSettingTable.UnEquipSkillBloc(nowEquipBloc, this);

        nowEquipBloc = null;
    }
}
