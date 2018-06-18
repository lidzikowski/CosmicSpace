using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Window
{
    [Header("Panel Header")]
    public Button CloseButton;
    public Transform ShipTransform;

    [Header("Equipment")]
    public GameObject EquipmentPrefab;
    private Transform Equipment;

    [Header("Warehouse")]
    public GameObject WarehousePrefab;
    private Transform Warehouse;

    public static GameObject ToolTip;

    private void Start()
    {
        ToolTip = GameObject.Find("ToolTip");
        ToolTip.SetActive(false);

        ButtonListener(CloseButton, CloseButton_Click);
        Refresh();
    }



    public override void Refresh()
    {
        Restart();
        Equipment.GetComponent<Equipment>().Refresh();
        Warehouse.GetComponent<Warehouse>().Refresh();
    }

    private void Restart()
    {
        foreach (Transform t in ShipTransform)
            Destroy(t.gameObject);

        Equipment = Instantiate(EquipmentPrefab, ShipTransform).transform;
        Equipment.gameObject.name = "Equipment";
        Warehouse = Instantiate(WarehousePrefab, ShipTransform).transform;
        Warehouse.gameObject.name = "Warehouse";
    }

    private void CloseButton_Click()
    {
        Gui.CloseWindow(Windows.Inventory);
    }

    public static void ShowToolTip(Vector2 position, Item item)
    {
        ToolTip.SetActive(true);
        ToolTip.transform.position = position;
        ToolTip.GetComponent<ToolTip>().Refresh(item);
    }

    public static void HideToolTip()
    {
        ToolTip.SetActive(false);
    }
}