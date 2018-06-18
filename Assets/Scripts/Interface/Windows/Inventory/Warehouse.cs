using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ItemSlot;
    
    public void Refresh()
    {
        CreateSlot(GameData.LocalPlayer.Items);
    }

    private void CreateSlot(List<Item> items)
    {
        foreach (Item item in items.Where(o => !o.Equipped))
        {
            GameObject go = Instantiate(ItemSlot, transform);
            go.GetComponent<ItemSlot>().ItemInSlot(item);
        }
    }
}