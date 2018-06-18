using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    [Header("Enemies")]
    public Transform EnemiesTransform;
    public List<MapEnemy> Enemies;

    [Header("Boxes")]
    public Transform BoxesTransform;
    public List<MapBox> Boxes;

    [Header("Portals")]
    public Transform PortalsTransform;
    public List<MapPortal> Portals;

    [Header("Backgrounds")]
    public Transform BackgroundTransform;
    public List<MapBackground> Backgrounds;



    protected virtual void Start()
    {
        SpawnEnemies();
        SpawnBoxes();
        SpawnPortals();
        SpawnBackgrounds();
    }

    protected virtual void LateUpdate()
    {

    }



    protected GameObject CreateObject(GameObject go, Vector3 position, Transform t)
    {
        return Instantiate(go, position, Quaternion.identity, t);
    }

    private void SpawnEnemies()
    {
        foreach (MapEnemy enemy in Enemies)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                CreateObject(GameData.EnemyShipObjects[enemy.EnemyType], Game.RandomPosition(), EnemiesTransform);
            }
        }
    }

    private void SpawnBoxes()
    {
        foreach (MapBox box in Boxes)
        {
            for (int i = 0; i < box.Count; i++)
            {
                //CreateObject(GameData.Boxes[box.BoxType], GameData.RandomPosition(), EnemiesTransform);
            }
        }
    }

    private void SpawnPortals()
    {
        foreach (MapPortal portal in Portals)
        {
            GameObject p = CreateObject(GameData.PortalObjects[portal.PortalType], Portal.Position(portal.Position), EnemiesTransform);
            p.GetComponent<Portal>().TargetMap = portal.TargetMap;
            p.GetComponent<Portal>().TargetPosition = Portal.Position(portal.TargetPosition);
        }
    }

    private void SpawnBackgrounds()
    {
        foreach (MapBackground background in Backgrounds)
        {
            CreateObject(Resources.Load<GameObject>("Backgrounds/" + background.BackgroundType), background.Position, BackgroundTransform);
        }
    }
}