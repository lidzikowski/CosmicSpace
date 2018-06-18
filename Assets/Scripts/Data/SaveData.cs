using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Scrap;
    public int Metal;
    public int Experience;
    public int Level;
    public int Map;
    public float PositionX;
    public float PositionY;

    public List<SaveShip> Ships;
    public List<int> Lasers;
    public List<int> Rockets;
    public int[] SelectedAmmo;
    public List<Item> PlayerItems;

    public SaveData()
    {
        Scrap = 100000;
        Metal = 100;
        Experience = 0;
        Level = 50;
        Map = 0;
        PositionX = 50;
        PositionY = -50;

        Lasers = new List<int>();
        for (int i = 0; i < Enum.GetNames(typeof(LaserBullets)).Length; i++)
        {
            if (i == 0)
                Lasers.Add(1000);
            else
                Lasers.Add(0);
        }

        Rockets = new List<int>();
        for (int i = 0; i < Enum.GetNames(typeof(RocketBullets)).Length; i++)
        {
            if (i == 0)
                Rockets.Add(100);
            else
                Rockets.Add(0);
        }

        Ships = new List<SaveShip>();
        for (int i = 0; i < Enum.GetNames(typeof(PlayerShips)).Length; i++)
        {
            List<Upgrade>[] upgrades = Game.ShipUpgrades((PlayerShips)i);
            SaveShip ship = new SaveShip();
            ship.UpgradeHitpoints = upgrades[0];
            ship.UpgradeSpeed = upgrades[1];
            if (i == 0)
            {
                ship.Bought = true;
                ship.Used = true;
            }
            else
            {
                ship.Bought = false;
                ship.Used = false;
            }
            Ships.Add(ship);
        }

        SelectedAmmo = new int[] { 0, 0 };

        PlayerItems = new List<Item>();
        Item item = GameData.ItemList[0].Clone();
        item.Equipped = true;
        PlayerItems.Add(item);
    }

    public SaveData(Player pl)
    {
        Scrap = pl.Scrap;
        Metal = pl.Metal;
        Experience = pl.Experience;
        Level = pl.Level;
        Map = pl.Map;
        PositionX = pl.transform.position.x;
        PositionY = pl.transform.position.y;

        Lasers = new List<int>();
        foreach (int i in pl.Lasers)
        {
            Lasers.Add(i);
        }

        Rockets = new List<int>();
        foreach (int i in pl.Rockets)
        {
            Rockets.Add(i);
        }

        Ships = new List<SaveShip>();
        foreach (HangarShip hangar in pl.Ships.Values)
        {
            SaveShip ship = new SaveShip();
            ship.UpgradeHitpoints = hangar.UpgradeHitpoints;
            ship.UpgradeSpeed = hangar.UpgradeSpeed;
            ship.Bought = hangar.Bought;
            ship.Used = hangar.Used;
            Ships.Add(ship);
        }

        SelectedAmmo = pl.SelectedAmmo;

        PlayerItems = pl.Items;
    }
}

[Serializable]
public class SaveShip
{
    public List<Upgrade> UpgradeHitpoints;
    public List<Upgrade> UpgradeSpeed;
    public bool Bought;
    public bool Used;
}