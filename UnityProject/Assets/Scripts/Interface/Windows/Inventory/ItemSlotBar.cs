using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotBar : MonoBehaviour
{
    public Text BarName;
    public Transform SlotsTransform;
    public GameObject ItemSlot;
    
    public void CreateItemsInBar(ItemTypes itemType)
    {
        List<Item> items = GameData.LocalPlayer.Items.Where(o => o.Equipped && o.Type == itemType).ToList();
        
        foreach (Item item in items)
        {
            GameObject go = Instantiate(ItemSlot, SlotsTransform);
            go.GetComponent<ItemSlot>().ItemInSlot(item);
        }
        
        string bonus = "";
        string barName = itemType + ":   (" + items.Count + "/";
        switch (itemType)
        {
            case ItemTypes.Laser:
                barName += GameData.LocalPlayer.Ship.LaserSlots;
                int damage = GameData.LocalPlayer.Ship.Damage;
                int range = Game.DamageRange(damage);
                bonus = string.Format("<{0},{1}> ~ {2}", damage - range, damage + range, damage);
                break;
            case ItemTypes.Shield:
                barName += GameData.LocalPlayer.Ship.ShieldSlots;
                bonus = GameData.LocalPlayer.Ship.MaxShields.ToString();
                break;
            case ItemTypes.Engine:
                barName += GameData.LocalPlayer.Ship.EngineSlots;
                bonus = GameData.LocalPlayer.Ship.Speed.ToString();
                break;
        }
        BarName.text = barName + ")   [";
        BarName.text += bonus.ToString() + "]";
    }
}