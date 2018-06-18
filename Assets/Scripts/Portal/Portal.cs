using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Portals
{
    
}

public enum PortalPositions
{
    TopLeft, Top, TopRight,
    CenterLeft, Center, CenterRight,
    BottomLeft, Bottom, BottomRight
}

public class Portal : MonoBehaviour
{
    [Header("Properties")]
    public Maps TargetMap;
    public Vector3 TargetPosition;
    public int RequiredLevel;



    void Start()
    {

    }
    
    void Update()
    {

    }

    public static Vector3 Position(PortalPositions position)
    {
        switch (position)
        {
            case PortalPositions.TopLeft:
                return new Vector3(50, -50);
            case PortalPositions.Top:
                return new Vector3(400, -400);
            case PortalPositions.TopRight:
                return new Vector3(750, -750);
            case PortalPositions.CenterLeft:
                return new Vector3(50, -50);
            case PortalPositions.Center:
                return new Vector3(400, -400);
            case PortalPositions.CenterRight:
                return new Vector3(750, -750);
            case PortalPositions.BottomLeft:
                return new Vector3(50, -50);
            case PortalPositions.Bottom:
                return new Vector3(400, -400);
            case PortalPositions.BottomRight:
                return new Vector3(750, -750);
        }
        return Vector3.zero;
    }
}