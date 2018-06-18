using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gui : MonoBehaviour
{
    public static Dictionary<Windows, WindowInstance> windows = new Dictionary<Windows, WindowInstance>();
    protected static Transform canvas;
    public static bool Ready = false;

    void Start()
    {
        for (int i = 0; i < Windows.GetNames(typeof(Windows)).Length; i++)
        {
            windows.Add((Windows)i, new WindowInstance());
        }

        canvas = transform;

        foreach (Transform t in transform)
            Destroy(t.gameObject);

        Ready = true;
    }

    public static void OpenWindow(Windows windowType)
    {
        InstantiateWindow(windowType, true);
    }

    public static void CloseWindow(Windows windowType)
    {
        InstantiateWindow(windowType, false);
    }

    public static void CloseAllWindow()
    {
        foreach (WindowInstance window in windows.Values.Where(o => o.Active))
        {
            window.Window = null;
        }
    }

    private static void InstantiateWindow(Windows windowType, bool status)
    {
        WindowInstance window = windows[windowType];

        if (window.Active == status)
            return;

        CloseAllWindow();

        if (status)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Windows/" + windowType), canvas);
            window.Window = go;
        }
        else
            window.Window = null;
    }
}