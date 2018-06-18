using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hangar : Window
{
    [Header("Panel Header")]
    public Button CloseButton;


    [Header("Ship statistics")]
    public Text ShipName;
    public RawImage ShipImage;
    public Transform ShipStatistics;
    public GameObject StatisticWithoutUpgradePrefab;
    public GameObject StatisticWithUpgradePrefab;

    [Header("Ship List")]
    public Transform ShipList;
    public GameObject ShipInListPrefab;
    public GameObject ShipInListWithBuyPrefab;



    private void Start()
    {
        ButtonListener(CloseButton, CloseButton_Click);
        Refresh();
    }


    
    public override void Refresh()
    {
        Restart(ShipStatistics);
        Restart(ShipList);

        foreach (HangarShip ship in GameData.LocalPlayer.Ships.Values.ToList().OrderBy(o => o.Ship.Level))
        {
            if(ship.Used)
            {
                CreateUsedShip(ship);
            }
            if (!ship.Bought && GameData.LocalPlayer.Level >= ship.Ship.Level)
                CreateShipInList(ship, true);
            else
                CreateShipInList(ship, false);
        }
    }

    private void Restart(Transform layer)
    {
        foreach (Transform t in layer)
            Destroy(t.gameObject);
    }

    private void CreateUsedShip(HangarShip hangar)
    {
        ShipName.text = hangar.Ship.Name;
        //ShipImage
        if(hangar.UpgradeHitpoints.Count > 0)
            CreatePrefabWithUpgrade(Upgrades.Hitpoints, hangar);
        else
            CreatePrefabWithoutUpgrade(Upgrades.Hitpoints, hangar.Ship.MaxHitpoints);

        if (hangar.UpgradeSpeed.Count > 0)
            CreatePrefabWithUpgrade(Upgrades.Speed, hangar);
        else
            CreatePrefabWithoutUpgrade(Upgrades.Speed, hangar.Ship.Speed);
    }

    private void CreatePrefabWithUpgrade(Upgrades upgrade, HangarShip hangar)
    {
        GameObject go = Instantiate(StatisticWithUpgradePrefab, ShipStatistics);
        go.GetComponent<StatisticWithUpgrade>().SetUpgrades(upgrade, hangar);
    }

    private void CreatePrefabWithoutUpgrade(Upgrades upgrade, int hitpoints)
    {
        GameObject go = Instantiate(StatisticWithoutUpgradePrefab, ShipStatistics);
        go.GetComponent<StatisticWithoutUpgrade>().Refresh(upgrade, hitpoints);
    }

    private void CreateShipInList(HangarShip ship, bool upgrade)
    {
        if(upgrade)
        {
            GameObject go = Instantiate(ShipInListWithBuyPrefab, ShipList);
            go.GetComponent<ShipInListWithBuy>().Refresh(ship);
            ButtonListener(go.AddComponent<Button>(), go.GetComponent<ShipInListWithBuy>().SwitchShip);
        }
        else
        {
            GameObject go = Instantiate(ShipInListPrefab, ShipList);
            go.GetComponent<ShipInList>().Refresh(ship);
            ButtonListener(go.AddComponent<Button>(), go.GetComponent<ShipInList>().SwitchShip);
        }
    }

    private void CloseButton_Click()
    {
        Gui.CloseWindow(Windows.Hangar);
    }
}