using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public static List<Upgrade>[] ShipUpgrades(PlayerShips shipName)
    {
        List<Upgrade>[] upgrades = new List<Upgrade>[2];

        List<Upgrade> hitpoints = null;
        List<Upgrade> speed = null;

        switch (shipName)
        {
            case PlayerShips.Avalon:
                hitpoints = new List<Upgrade>();
                speed = new List<Upgrade>();

                hitpoints = UpgradeGenerator(shipName);
                speed = UpgradeGenerator(shipName, false, 10);

                break;

            case PlayerShips.Templar:
                hitpoints = new List<Upgrade>();
                speed = new List<Upgrade>();

                hitpoints = UpgradeGenerator(shipName);
                speed = UpgradeGenerator(shipName, false, 0);

                break;

            case PlayerShips.Invictus:
                hitpoints = new List<Upgrade>();
                speed = new List<Upgrade>();

                hitpoints = UpgradeGenerator(shipName);
                speed = UpgradeGenerator(shipName, false, 20);

                break;
        }

        upgrades[0] = hitpoints;
        upgrades[1] = speed;

        return upgrades;
    }

    public static List<Upgrade> UpgradeGenerator(PlayerShips shipName, bool hitpoint = true, int count = 10)
    {
        List<Upgrade> upgrades = new List<Upgrade>();
        PlayerShip ship = GameData.PlayerShipObjects[shipName].GetComponent<PlayerShip>();
        for (int i = 0; i < count; i++)
        {
            double ii = i + 1 * 1.334;
            Upgrades upgradeType;
            int bonus;
            int costMetal;
            int requiredLevel;
            if (hitpoint)
            {
                upgradeType = Upgrades.Hitpoints;
                bonus = (int)(ii * ship.Level * 75.22);
                costMetal = (int)(ii * ship.Level * 5.18);
                requiredLevel = ship.Level + (i / 4);
            }
            else
            {
                upgradeType = Upgrades.Speed;
                bonus = (int)ii;
                costMetal = (int)(ii * 13.71);
                requiredLevel = ship.Level + (i / 8);
            }
            int costScrap = costMetal * GameData.MetalPrice;

            upgrades.Add(new Upgrade(upgradeType, bonus, costScrap, costMetal, requiredLevel));
        }
        return upgrades;
    }



    public static Vector3 RandomPosition()
    {
        int x = Random.Range(0, 1000);
        int y = Random.Range(0, 800);
        return new Vector3(x, -y);
    }

    public static Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos = new Vector3();
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public static int Damage(int damage, Ammunition ammunition)
    {
        int dmg;
        if (ammunition.Precision == 100)
        {
            int range = DamageRange(damage);
            dmg = (int)(Random.Range(damage - range, damage + range) * ammunition.Multiplier);
        }
        else
        {
            if (Random.Range(1, 101) <= ammunition.Precision)
            {
                int range = DamageRange(damage);
                dmg = (int)(Random.Range(damage - range, damage + range) * ammunition.Multiplier);
            }
            else
            {
                dmg = -1;
            }
        }

        return dmg;
    }

    public static int DamageRange(int damage)
    {
        return (int)(damage * 0.1);
    }

    public static Color32 ColorItemQuality(Item item)
    {
        Color32 color = Color.white;
        switch (item.Quality)
        {
            case ItemQuality.Normal:
                color = new Color32(191, 191, 191, 200);
                break;
            case ItemQuality.Rare:
                color = new Color32(196, 77, 255, 100);
                break;
            case ItemQuality.Unique:
                color = new Color32(255, 173, 51, 100);
                break;
        }
        return color;
    }
}