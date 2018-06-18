using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public abstract class PlayerShip : Ship
{
    [Header("Prices")]
    public int CostScrap;
    public int CostMetal;

    [Header("Equipment")]
    public int LaserSlots;
    public int ShieldSlots;
    public int EngineSlots;
    
    public int ShotRange
    {
        get
        {
            int range = 0;
            int count = 0;

            foreach (Item item in GameData.LocalPlayer.Items.Where(o => o.Equipped && o.Type == ItemTypes.Laser))
            {
                range += item.ShotRange;
                count++;
            }

            if (count == 0)
                return 0;
            return range / count;
        }
    }
    public override int MaxHitpoints
    {
        get
        {
            int equipment = 0, shipBonus = 0;
            
            Upgrade upgrade = GameData.LocalPlayer.Ships[GameData.LocalPlayer.ShipName].UpgradeHitpoints.FindLast(o => o.Bought);
            if (upgrade != null)
                shipBonus = (int)upgrade.Bonus;

            return base.MaxHitpoints + equipment + shipBonus;
        }
    }
    public override int MaxShields
    {
        get
        {
            float equipment = 0;

            foreach (Item item in GameData.LocalPlayer.Items.Where(o => o.Equipped && o.Type == ItemTypes.Shield))
            {
                equipment += item.Bonus;
            }

            return base.MaxShields + (int)equipment;
        }
    }
    public override int Speed
    {
        get
        {
            float equipment = 0;
            int shipBonus = 0;

            Upgrade upgrade = GameData.LocalPlayer.Ships[GameData.LocalPlayer.ShipName].UpgradeSpeed.FindLast(o => o.Bought);
            if (upgrade != null)
                shipBonus = (int)upgrade.Bonus;

            foreach (Item item in GameData.LocalPlayer.Items.Where(o => o.Equipped && o.Type == ItemTypes.Engine))
            {
                equipment += item.Bonus;
            }

            return base.Speed + (int)equipment + shipBonus;
        }
        set
        {
            base.Speed = value;
        }
    }
    public int Damage
    {
        get
        {
            float equipment = 0;

            foreach (Item item in GameData.LocalPlayer.Items.Where(o => o.Equipped && o.Type == ItemTypes.Laser))
            {
                equipment += item.Bonus;
            }

            return (int)equipment;
        }
    }
    protected AudioSource EngineSound;
    protected override bool Engines
    {
        get
        {
            return engine;
        }
        set
        {
            if (value == engine)
                return;

            if (value)
                EngineSound.mute = false;
            else
                EngineSound.mute = true;
            engine = value;
        }
    }



    protected override void Start()
    {
        EngineSound = GetComponent<AudioSource>();
        Name = GameData.LocalPlayer.Username;

        foreach (LaserBullets laser in Enum.GetValues(typeof(LaserBullets)))
        {
            AmmunitionsLaser.Add(laser);
        }
        foreach (RocketBullets rocket in Enum.GetValues(typeof(RocketBullets)))
        {
            AmmunitionsRocket.Add(rocket);
        }

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Fly(GameData.LocalPlayer.gameObject);
        Rotate(gameObject);

        MouseControl();
        KeyControl();
    }


    protected void MouseControl()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            Controller();
        else if (Input.GetMouseButton(0))
            Controller(false);
    }

    protected void KeyControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (!Attack)
            {
                if (Target != null)
                {
                    Attack = true;
                    Target.GetComponent<Ship>().Target = gameObject;
                    Target.GetComponent<Ship>().Attack = true;
                }
                else
                    Debug.Log("Brak celu");
            }
            else
                Attack = false;
        }
    }

    protected override void ShotFire()
    {
        if (Target == null)
        {
            Attack = false;
            return;
        }
        if (Attack)
        {
            bool canShot = Vector3.Distance(transform.position, Target.transform.position) <= ShotRange;

            // Lasers from laser gameobject
            Ammunition ammunition = Ammunitions[GameData.LocalPlayer.SelectedAmmo[0]];
            if (ammunition.Ready)
            {
                if (canShot)
                {
                    CreateBullet(Game.Damage(Damage, ammunition), ammunition, lasers);
                }
                else
                    Debug.LogError("Cel poza zasiegiem!");
            }

            // Rockets from ship transform
            //ammunition = Ammunitions[GameData.LocalPlayer.SelectedAmmo[1]];
            //if (ammunition.Ready && canShot)
            //{
            //    List<Transform> trans = new List<Transform>();
            //    trans.Add(transform);
            //    CreateBullet(Game.Damage((int)(1000 * ammunition.Multiplier), ammunition), ammunition, trans);
            //}

        }
    }

    protected override void Destroy()
    {
        Attack = false;
        Target = null;
        Hitpoints = MaxHitpoints / 10;

        MapGenerator.ChangeMap(Maps.Map0);

        GameData.LocalPlayer.transform.position = new Vector3(100, -100);
        NewPosition = GameData.LocalPlayer.transform.position;
    }

    private void Controller(bool press = true)
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!press)
                return;

            if (gameObject == hit.transform.gameObject)
            {
                return;
            }
            else if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
            {
                Attack = false;
                Target = hit.transform.gameObject;
            }
        }
        else
        {
            float x = (mousePosition.x - Screen.width / 2) / 20;
            float y = (mousePosition.y - Screen.height / 2) / 20;
            NewPosition = new Vector3(
                GameData.LocalPlayer.gameObject.transform.position.x + x,
                GameData.LocalPlayer.gameObject.transform.position.y + y);
        }
    }
}