using UnityEngine;
using UnityEngine.UI;

public class ShipInList : MonoBehaviour
{
    public Text ShipName;
    public Text Bought;
    protected PlayerShips shipType;

    public virtual void Refresh(HangarShip ship)
    {
        ShipName.text = ship.Ship.name;
        shipType = ship.Type;
        if (ship.Bought == true)
        {
            if(ship.Used)
                StatusMessage("Used");
            else
                StatusMessage("Bought");
        }
        else
            StatusMessage("Required level " + ship.Ship.Level);
    }

    public void SwitchShip()
    {
        Player player = GameData.LocalPlayer;
        if(player.ShipName != shipType)
            player.ChangeShip(shipType);
    }
    
    protected void StatusMessage(string message)
    {
        Bought.text = message;
    }
}