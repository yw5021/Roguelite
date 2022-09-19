using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSettingTable : MonoBehaviour
{
    private const int maxTableX = 4;
    private const int maxTableY = 4;

    [SerializeField]
    private SkillSettingCell[,] skillTable = new SkillSettingCell[maxTableX, maxTableY];

    private List<SkillSettingBloc> nowEquipedBloc = new List<SkillSettingBloc>();

    public SkillSettingCell[,] SkillTable
    {
        get
        {
            return skillTable;
        }
    }

    private void Awake()
    {
        Init();
    }

    public void CheckEquipSkillSettingBloc(SkillSettingBloc bloc, SkillSettingCell cell, bool isInit)
    {
        int x = cell.X;
        int y = cell.Y;

        BlocPosition[] blocPositions = bloc.PlayerSkillBloc.BlocPositions;

        for (int i = 0; i < blocPositions.Length; i++)
        {
            BlocPosition position = blocPositions[i];

            Debug.Log($"Check {x},{y}  {position.X},{position.Y} {x + position.X},{y + position.Y}");

            if (x + position.X < 0 || x + position.X >= maxTableX)
            {
                Debug.Log("Out min or max X");
                bloc.ResetPosition();
                return;
            }
            else if (y + position.Y < 0 || y + position.Y >= maxTableY)
            {
                Debug.Log("Out min or max Y");
                bloc.ResetPosition();
                return;
            }
            else if (CheckEquiped(x + position.X, y + position.Y))
            {
                Debug.Log("Already Equiped");
                bloc.ResetPosition();
                return;
            }
        }

        EquipSkillBloc(bloc, cell, isInit);
    }

    private void Init()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            SkillSettingCell cell = child.AddComponent<SkillSettingCell>();

            int x = i % 4;
            int y = i / 4;

            Debug.Log(child.name + " " +  x + " " + y);

            cell.Init(this, x, y);

            skillTable[x, y] = cell;
        }
    }

    private bool CheckEquiped(int x,int y)
    {
        if (skillTable[x, y].isEquiped)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EquipSkillBloc(SkillSettingBloc bloc, SkillSettingCell cell, bool isInit)
    {
        nowEquipedBloc.Add(bloc);

        int x = cell.X;
        int y = cell.Y;

        BlocPosition[] blocPositions = bloc.PlayerSkillBloc.BlocPositions;

        for (int i = 0; i < blocPositions.Length; i++)
        {
            BlocPosition position = blocPositions[i];

            skillTable[x + position.X, y + position.Y].isEquiped = true;
        }

        bloc.EquipSkillSettingBloc(cell,isInit);
    }

    public void UnEquipSkillBloc(SkillSettingBloc bloc, SkillSettingCell cell)
    {
        nowEquipedBloc.Remove(bloc);

        int x = cell.X;
        int y = cell.Y;

        BlocPosition[] blocPositions = bloc.PlayerSkillBloc.BlocPositions;

        for (int i = 0; i < blocPositions.Length; i++)
        {
            BlocPosition position = blocPositions[i];

            skillTable[x + position.X, y + position.Y].isEquiped = false;
        }
    }
}
