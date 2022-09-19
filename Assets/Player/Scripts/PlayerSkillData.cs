using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    None = 0,

    Active = 1,
    Passive = 2,
    Buff = 3
}

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/SkillData", order = int.MaxValue)]
public class PlayerSkillData : ScriptableObject
{
    [SerializeField]
    private int index;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string descript;

    [SerializeField]
    private SkillType skillType;

    public int Index
    {
        get
        {
            return index;
        }
    }
    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }
    public string SkillName
    {
        get
        {
            return skillName;
        }
    }
    public string Descript
    {
        get
        {
            return descript;
        }
    }
    public SkillType SkillType
    {
        get
        {
            return skillType;
        }
    }
}
