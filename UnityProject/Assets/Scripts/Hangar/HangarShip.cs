using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HangarShip
{
    public GameObject Model;
    public PlayerShip Ship;
    public PlayerShips Type;

    public List<Upgrade> UpgradeHitpoints = new List<Upgrade>(); //SAVE
    public List<Upgrade> UpgradeSpeed = new List<Upgrade>(); //SAVE
    public bool Bought; //SAVE
    public bool Used; //SAVE



    public HangarShip(GameObject model, PlayerShips type)
    {
        Model = model;
        Ship = model.GetComponent<PlayerShip>();
        Type = type;
    }



    public int CurrentUpgradeIndex(List<Upgrade> upgrades)
    {
        return upgrades.FindLastIndex(o => o.Bought);
    }

    public Upgrade CurrentUpgrade(List<Upgrade> upgrades)
    {
        return upgrades.FindLast(o => o.Bought);
    }

    public Upgrade NextUpgrade(List<Upgrade> upgrades)
    {
        try
        {
            return upgrades.First(o => !o.Bought);
        }
        catch
        {
            return null;
        }
    }

    public bool Upgrade(Upgrade upgrade, Player player, bool scrap)
    {
        if (scrap)
        {
            if (player.Scrap >= upgrade.CostScrap)
            {
                player.Scrap -= upgrade.CostScrap;
                BuyUpgrade(upgrade);
                return true;
            }
            else
                return false;
        }
        else
        {
            if (player.Metal >= upgrade.CostMetal)
            {
                player.Metal -= upgrade.CostMetal;
                BuyUpgrade(upgrade);
                return true;
            }
            else
                return false;
        }
    }

    private void BuyUpgrade(Upgrade upgrade)
    {
        upgrade.Bought = true;
        Debug.Log("Zakupiono ulepszenie do: " + upgrade.Type);
    }
}