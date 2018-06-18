using UnityEngine.UI;

public class ShipInListWithBuy : ShipInList
{
    public Button BuyScrap;
    public Button BuyMetal;

    private void Start()
    {
        Window.ButtonListener(BuyScrap, BuyForScrap);
        Window.ButtonListener(BuyMetal, BuyForMetal);
    }

    public override void Refresh(HangarShip ship)
    {
        ShipName.text = ship.Ship.name;
        shipType = ship.Type;
        if (ship.Bought == true)
            if (ship.Used)
                StatusMessage("Used");
            else
                StatusMessage("Bought");
        else
        {
            string costScrap = "Buy for " + ship.Ship.CostScrap + " Scrap";
            string costMetal = "Buy for " + ship.Ship.CostMetal + " Metal";
            if (GameData.LocalPlayer.Level >= ship.Ship.Level)
            {
                ButtonText(true, costScrap, costMetal);
                StatusMessage("Available");
            }
            else
            {
                ButtonText(false, costScrap, costMetal);
                StatusMessage("Required level " + ship.Ship.Level);
            }
        }
    }

    private void ButtonText(bool status, string scrap, string metal)
    {
        BuyScrap.interactable = status;
        BuyMetal.interactable = status;

        BuyScrap.transform.GetChild(0).GetComponent<Text>().text = scrap;
        BuyMetal.transform.GetChild(0).GetComponent<Text>().text = metal;
    }

    private void BuyForScrap()
    {
        GameData.LocalPlayer.BuyShip(shipType, true);
    }

    private void BuyForMetal()
    {
        GameData.LocalPlayer.BuyShip(shipType, false);
    }
}