using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "Scriptable Object/ArtifactData", order = int.MaxValue)]
public class PlayerArtifactData : ScriptableObject
{
    [SerializeField]
    private int index;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int price;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string descript;

    [SerializeField]
    private string effectType;
    [SerializeField]
    private float effectBaseValue;
    [SerializeField]
    private float effectStackValue;

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
    public int Price
    {
        get
        {
            return price;
        }
    }
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }
    public string Descript
    {
        get
        {
            return descript;
        }
    }

    public string EffectType
    {
        get
        {
            return effectType;
        }
    }
    public float EffectBaseValue
    {
        get
        {
            return effectBaseValue;
        }
    }
    public float EffectStackValue
    {
        get
        {
            return effectStackValue;
        }
    }
}