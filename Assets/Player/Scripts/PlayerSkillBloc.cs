using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocPosition
{
    private int x;
    private int y;

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

    public BlocPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PlayerSkillBloc
{
    public const int maxBlocLevel = 4;

    private enum SymmetryType
    {
        Normal = 0,
        Horizontal = 1,
        Vertical = 2,
        All = 3
    }

    private enum Dir
    {
        Horizontal = 0,
        Vertical = 1
    }

    private int index;
    private BlocPosition[] blocPositions;

    public int Index
    {
        get
        {
            return index;
        }
    }
    public BlocPosition[] BlocPositions
    {
        get
        {
            return blocPositions;
        }
    }

    public PlayerSkillBloc(int blocLevel)
    {
        PlayerSkillData skillData = GameManager.Instance.SkillDatas[Random.Range(0,GameManager.Instance.SkillDatas.Length)];

        index = skillData.Index;

        CreateBlocPattern(blocLevel);
    }
    public PlayerSkillBloc(int index,int blocLevel)
    {
        this.index = index;

        CreateBlocPattern(blocLevel);
    }

    #region BlocPattern
    private void CreateBlocPattern(int blocLevel)
    {
        BlocPosition[] blocPositions = new BlocPosition[maxBlocLevel - (blocLevel - 1)];

        switch (blocLevel)
        {
            case 1:
                CreateTetrominoPattern(blocPositions);
                break;

            case 2:
                CreateTriominoPattern(blocPositions);
                break;

            case 3:
                CreateDominoPattern(blocPositions);
                break;

            case 4:
                CreateMonominoPattern(blocPositions);
                break;

            default:
                Debug.LogError("Error 69548 - BlocLevel Error");
                break;
        }

        this.blocPositions = blocPositions;
    }

    private void CreateMonominoPattern(BlocPosition[] blocPositions)
    {
        blocPositions[0] = new BlocPosition(0, 0);
    }

    private void CreateDominoPattern(BlocPosition[] blocPositions)
    {
        blocPositions[0] = new BlocPosition(0, 0);

        switch (CommonFunc.RandomEnum<Dir>())
        {
            case Dir.Horizontal:
                blocPositions[1] = new BlocPosition(1, 0);
                break;

            case Dir.Vertical:
                blocPositions[1] = new BlocPosition(0, 1);
                break;
        }
    }

    private void CreateTriominoPattern(BlocPosition[] blocPositions)
    {
        blocPositions[0] = new BlocPosition(0, 0);

        int pattern = Random.Range(0, 2); // 0 - I 1 - L

        switch (pattern)
        {
            case 0:
                switch (CommonFunc.RandomEnum<Dir>())
                {
                    case Dir.Horizontal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        break;

                    case Dir.Vertical:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(0, 1);
                        break;
                }
                break;

            case 1:
                switch (CommonFunc.RandomEnum<SymmetryType>())
                {
                    case SymmetryType.Normal:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(1, 0);
                        break;

                    case SymmetryType.Horizontal:
                        blocPositions[1] = new BlocPosition(0, 1);
                        blocPositions[2] = new BlocPosition(1, 0);
                        break;

                    case SymmetryType.Vertical:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(-1, 0);
                        break;

                    case SymmetryType.All:
                        blocPositions[1] = new BlocPosition(0, 1);
                        blocPositions[2] = new BlocPosition(-1, 0);
                        break;
                }
                break;
        }
    }

    private void CreateTetrominoPattern(BlocPosition[] blocPositions)
    {
        blocPositions[0] = new BlocPosition(0, 0);

        int pattern = Random.Range(0, 5); // 0 - I / 1 - O / 2 - S / 3 - L / 4 - T

        switch (pattern)
        {
            case 0:
                switch (CommonFunc.RandomEnum<Dir>())
                {
                    case Dir.Horizontal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(2, 0);
                        break;

                    case Dir.Vertical:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(0, 1);
                        blocPositions[3] = new BlocPosition(0, 2);
                        break;
                }
                break;

            case 1:
                blocPositions[1] = new BlocPosition(1, 0);
                blocPositions[2] = new BlocPosition(0, 1);
                blocPositions[3] = new BlocPosition(1, 1);
                break;

            case 2:
                switch (CommonFunc.RandomEnum<SymmetryType>())
                {
                    case SymmetryType.Normal:
                        blocPositions[1] = new BlocPosition(1, 0);
                        blocPositions[2] = new BlocPosition(0, 1);
                        blocPositions[3] = new BlocPosition(-1, 1);
                        break;

                    case SymmetryType.Horizontal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(0, 1);
                        blocPositions[3] = new BlocPosition(1, 1);
                        break;

                    case SymmetryType.Vertical:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(-1, 0);
                        blocPositions[3] = new BlocPosition(-1, 1);
                        break;

                    case SymmetryType.All:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(1, 1);
                        break;
                }
                break;

            case 3:
                switch (CommonFunc.RandomEnum<SymmetryType>())
                {
                    case SymmetryType.Normal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(1, 1);
                        break;

                    case SymmetryType.Horizontal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(1, -1);
                        break;

                    case SymmetryType.Vertical:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(-1, 1);
                        blocPositions[3] = new BlocPosition(1, 0);
                        break;

                    case SymmetryType.All:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(-1, -1);
                        blocPositions[3] = new BlocPosition(1, 0);
                        break;
                }
                break;

            case 4:
                switch (CommonFunc.RandomEnum<SymmetryType>())
                {
                    case SymmetryType.Normal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(0, 1);
                        break;

                    case SymmetryType.Horizontal:
                        blocPositions[1] = new BlocPosition(-1, 0);
                        blocPositions[2] = new BlocPosition(1, 0);
                        blocPositions[3] = new BlocPosition(0, -1);
                        break;

                    case SymmetryType.Vertical:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(0, 1);
                        blocPositions[3] = new BlocPosition(1, 0);
                        break;

                    case SymmetryType.All:
                        blocPositions[1] = new BlocPosition(0, -1);
                        blocPositions[2] = new BlocPosition(0, 1);
                        blocPositions[3] = new BlocPosition(-1, 0);
                        break;
                }
                break;
        }
    }

    #endregion
}
