using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapPortal
{
    public Portals PortalType;
    public PortalPositions Position;
    public Maps TargetMap;
    public PortalPositions TargetPosition;
}