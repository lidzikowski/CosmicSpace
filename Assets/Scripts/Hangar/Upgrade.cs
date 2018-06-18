using System;

public enum Upgrades
{
    Hitpoints,
    Speed
}

[Serializable]
public class Upgrade
{
    public Upgrades Type;
    public float Bonus;
    public int CostScrap;
    public int CostMetal;
    public int RequiredLevel;
    public bool Bought;

    public Upgrade() { }
    public Upgrade(Upgrades type, int bonus, int costScrap, int costMetal, int requiredLevel)
    {
        Type = type;
        Bonus = bonus;
        CostScrap = costScrap;
        CostMetal = costMetal;
        RequiredLevel = requiredLevel;
        Bought = false;
    }
}