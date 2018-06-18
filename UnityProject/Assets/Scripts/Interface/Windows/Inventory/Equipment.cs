using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ItemSlotBar;



    public void Refresh()
    {
        CreateSlotBar(ItemTypes.Laser);
        CreateSlotBar(ItemTypes.Shield);
        CreateSlotBar(ItemTypes.Engine);
    }

    private void CreateSlotBar(ItemTypes itemType)
    {
        GameObject go = Instantiate(ItemSlotBar, transform);
        go.GetComponent<ItemSlotBar>().CreateItemsInBar(itemType);
    }
}