using UnityEngine;

public class WindowInstance
{
    private GameObject window;
    public GameObject Window
    {
        get
        {
            return window;
        }
        set
        {
            if (value != null)
            {
                window = value;
            }
            else
            {
                if (Active)
                    Script.Close();
                window = null;
            }
        }
    }
    public Window Script
    {
        get
        {
            return window.GetComponent<Window>();
        }
    }
    public bool Active
    {
        get
        {
            if (window != null)
                return true;
            else
                return false;
        }
    }
}