using System;
using UnityEngine;

[Serializable]
public class ItemReward
{
    public Items Item;
    [Range(0, 100)]
    public float Chance;
}