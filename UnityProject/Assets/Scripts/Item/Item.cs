using System;
using UnityEngine;

public enum ItemTypes
{
    Laser,
    Shield,
    Engine
}

public enum ItemQuality
{
    Normal,
    Rare,
    Unique
}

public enum Items
{
    StartLaser
}

[Serializable]
public class Item
{
    [Header("Properties")]
    public Items Name;
    public ItemTypes Type;
    public ItemQuality Quality;
    public int RequiredLevel;
    public float Bonus;
    public int Value;
    public string Description;
    public Sprite ItemImage;

    [Header("Laser")]
    [Range(20, 40)]
    public int ShotRange;

    [HideInInspector]
    public bool Equipped;
    [HideInInspector]
    public int UpgradeLevel = 1;

    public Item Clone()
    {
        return (Item)MemberwiseClone();
    }
}