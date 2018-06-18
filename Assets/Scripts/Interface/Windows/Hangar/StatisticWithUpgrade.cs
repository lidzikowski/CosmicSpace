using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticWithUpgrade : MonoBehaviour
{
    public Text Statistic;
    public Text Bonus;
    public Text UpgradeInfo;
    public Text UpgradeBonus;
    public Button BuyScrap;
    public Button BuyMetal;

    private Upgrades UpgradeType;
    private HangarShip Hangar;

    private void Start()
    {
        Window.ButtonListener(BuyScrap, BuyForScrap);
        Window.ButtonListener(BuyMetal, BuyForMetal);
    }

    public void SetUpgrades(Upgrades upgradeType, HangarShip hangar)
    {
        UpgradeType = upgradeType;
        Hangar = hangar;
        Refresh();
    }

    public void Refresh()
    {
        if (UpgradeType == Upgrades.Hitpoints)
        {
            Create(Hangar.UpgradeHitpoints);
        }
        else if (UpgradeType == Upgrades.Speed)
        {
            Create(Hangar.UpgradeSpeed);
        }
    }

    private void Create(List<Upgrade> upgrades)
    {
        string bonus = "", upgradeInfo = "", upgradeBonus = "";
        Upgrade currentUpgrade = Hangar.CurrentUpgrade(upgrades);
        Upgrade nextUpgrade = Hangar.NextUpgrade(upgrades);

        if (UpgradeType == Upgrades.Hitpoints)
        {
            Statistic.text = UpgradeType + " (basic " + Hangar.Ship.maxHitpoints + ")";
            bonus = Hangar.Ship.MaxHitpoints.ToString();
        }
        else if (UpgradeType == Upgrades.Speed)
        {
            Statistic.text = UpgradeType + " (basic " + Hangar.Ship.speed + ")";
            bonus = Hangar.Ship.Speed.ToString();
        }

        upgradeInfo = "Upgrade " + (Hangar.CurrentUpgradeIndex(upgrades) + 1) + " / " + upgrades.Count;

        bool cupgrade = currentUpgrade != null;
        bool nupgrade = nextUpgrade != null;
        
        if (cupgrade)
            upgradeBonus += currentUpgrade.Bonus.ToString();
        if (cupgrade && nupgrade)
            upgradeBonus += " > ";
        else if (!cupgrade && nupgrade)
            upgradeBonus += "0 > ";
        if (nupgrade)
        {
            upgradeBonus += nextUpgrade.Bonus.ToString();
            string costScrap = "Buy for " + nextUpgrade.CostScrap + " Scrap";
            string costMetal = "Buy for " + nextUpgrade.CostMetal + " Metal";
            ButtonsText(costScrap, costMetal);
        }
        else
        {
            BuyScrap.interactable = false;
            BuyMetal.interactable = false;
            ButtonsText("MAX LEVEL", "MAX LEVEL");
        }

        Bonus.text = bonus;
        UpgradeInfo.text = upgradeInfo;
        UpgradeBonus.text = upgradeBonus;
    }

    private void ButtonsText(string scrap, string metal)
    {
        BuyScrap.transform.GetChild(0).GetComponent<Text>().text = scrap;
        BuyMetal.transform.GetChild(0).GetComponent<Text>().text = metal;
    }

    private void BuyForScrap()
    {
        Buy(true);
    }

    private void BuyForMetal()
    {
        Buy(false);
    }

    private void Buy(bool scrap)
    {
        if (UpgradeType == Upgrades.Hitpoints)
        {
            BuyUpgrade(Hangar.UpgradeHitpoints, scrap);
        }
        else if (UpgradeType == Upgrades.Speed)
        {
            BuyUpgrade(Hangar.UpgradeSpeed, scrap);
        }
    }

    private void BuyUpgrade(List<Upgrade> upgrades, bool scrap)
    {
        Upgrade upgrade = Hangar.NextUpgrade(upgrades);
        if (upgrade != null)
            Hangar.Upgrade(upgrade, GameData.LocalPlayer, scrap);
        Refresh();
    }
}