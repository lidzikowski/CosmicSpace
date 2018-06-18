using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Windows
{
    MainMenu, Hangar, Inventory
}

public abstract class Window : MonoBehaviour
{
    public virtual void Close()
    {
        Destroy(gameObject);
    }

    public virtual void Refresh() { }

    public static void ButtonListener(Button button, UnityAction function)
    {
        if (button != null)
            button.onClick.AddListener(function);
    }
}