using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Maps
{
    Map0
}

public enum Backgrounds
{
    Stars
}

public class MapGenerator : MonoBehaviour
{
    public static Transform MapTransform;

    private void Start()
    {
        MapTransform = transform;
    }

    public static void ChangeMap(Maps map)
    {
        foreach(Transform t in MapTransform)
            Destroy(t.gameObject);

        Instantiate(Resources.Load<GameObject>("Maps/" + map), MapTransform);
    }
}