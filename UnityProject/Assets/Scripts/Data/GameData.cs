using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int MetalPrice = 3;
    public static int AverageShotDistance = 30;
    public static int BossMulti = 5;
    public static int FlySpeed = 15;
    public static int RotateSpeed = 8;
    public static float RepairTime = 5;

    public static GameObject PlayerPrefab;
    public static Player LocalPlayer;

    public static Dictionary<PlayerShips, GameObject> PlayerShipObjects = new Dictionary<PlayerShips, GameObject>();
    public static Dictionary<EnemyShips, GameObject> EnemyShipObjects = new Dictionary<EnemyShips, GameObject>();
    public static Dictionary<LaserBullets, GameObject> LaserObjects = new Dictionary<LaserBullets, GameObject>();
    public static Dictionary<RocketBullets, GameObject> RocketObjects = new Dictionary<RocketBullets, GameObject>();
    public static Dictionary<Portals, GameObject> PortalObjects = new Dictionary<Portals, GameObject>();

    [Header("Items:")]
    public List<Item> Items = new List<Item>();
    public static Dictionary<Items, Item> ItemList;

    IEnumerator Start()
    {
        PlayerPrefab = Resources.Load<GameObject>("Player");

        ItemList = Items.ToDictionary(o => o.Name, o => o);

        LoadPlayerShips();
        LoadEnemieShips();
        LoadLasers();
        LoadRockets();

        yield return new WaitUntil(() => Gui.Ready);

        Gui.OpenWindow(Windows.MainMenu);
    }

    private void LoadLasers()
    {
        foreach (LaserBullets bullet in Enum.GetValues(typeof(LaserBullets)))
        {
            GameObject go = Resources.Load<GameObject>("Bullets/Lasers/" + bullet);
            LaserObjects.Add(bullet, go);
        }
    }

    private void LoadRockets()
    {
        foreach (RocketBullets bullet in Enum.GetValues(typeof(RocketBullets)))
        {
            GameObject go = Resources.Load<GameObject>("Bullets/Rockets/" + bullet);
            RocketObjects.Add(bullet, go);
        }
    }
    
    private void LoadPlayerShips()
    {
        foreach (PlayerShips ship in Enum.GetValues(typeof(PlayerShips)))
        {
            GameObject go = Resources.Load<GameObject>("Ships/Player/" + ship);
            PlayerShipObjects.Add(ship, go);
        }
    }

    private void LoadEnemieShips()
    {
        foreach (EnemyShips ship in Enum.GetValues(typeof(EnemyShips)))
        {
            GameObject go = Resources.Load<GameObject>("Ships/Enemie/" + ship);
            EnemyShipObjects.Add(ship, go);
        }
    }
}