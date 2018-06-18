using System;
using System.Collections.Generic;

[Serializable]
public class Reward
{
    public int Scrap;
    public int Metal;
    public int Experience;
    public List<ItemReward> Items;
}