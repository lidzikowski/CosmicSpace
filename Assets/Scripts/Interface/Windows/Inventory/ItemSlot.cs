using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image ItemImage;
    public Text UpgradeLevel;

    private Item item = null;
    private bool click = false;
    


    public void ItemInSlot(Item item)
    {
        this.item = item;
        ItemImage.sprite = item.ItemImage;
        UpgradeLevel.text = item.UpgradeLevel.ToString();

        GetComponent<Image>().color = Game.ColorItemQuality(item);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
        Inventory.HideToolTip();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        click = false;
        Inventory.HideToolTip();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (click)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool equipment = eventData.position.x <= Screen.width / 2;

        if(equipment)
        {
            if (!item.Equipped)
            {
                GameData.LocalPlayer.UseItem(item);
            }
        }
        else
        {
            if (item.Equipped)
            {
                item.Equipped = false;
            }
        }

        Gui.windows[Windows.Inventory].Script.Refresh();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || click)
            return;

        Vector2 position = GetComponent<RectTransform>().position;
        Vector2 size = Inventory.ToolTip.GetComponent<RectTransform>().sizeDelta;
        Inventory.ShowToolTip(new Vector3(position.x + (size.x / 2.8f), position.y - (size.y / 4.5f)), item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.HideToolTip();
    }
}