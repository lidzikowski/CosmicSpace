using System.Collections.Generic;
using UnityEngine;

public enum EnemyShips {
    Bravery, BossBravery,
    Helios, BossHelios
}
public enum PlayerShips {
    Avalon,
    Invictus,
    Templar
}

public abstract class Ship : MonoBehaviour
{
    [Header("Name")]
    public string Name;

    [Header("Statistics")]
    public int Hitpoints;
    public int maxHitpoints;
    public virtual int MaxHitpoints { get { return maxHitpoints; } }
    public int Shields;
    public int maxShields;
    public virtual int MaxShields { get { return maxShields; } }

    [Range(200, 600)]
    public int speed;
    public virtual int Speed { get { return speed; } set { speed = value; } }
    public int Level;

    [Header("Ammunitions")]
    public List<LaserBullets> AmmunitionsLaser = new List<LaserBullets>();
    public List<RocketBullets> AmmunitionsRocket = new List<RocketBullets>();
    protected List<Ammunition> Ammunitions = new List<Ammunition>();

    [Header("Transforms")]
    public GameObject ShipGameObject;
    public GameObject ShipNameGameObject;
    public Transform EngineTransform;
    public Transform LaserTransform;

    protected List<ParticleSystem> engines = new List<ParticleSystem>();
    protected bool engine;
    protected virtual bool Engines
    {
        get
        {
            return engine;
        }
        set
        {
            if (value == engine)
                return;

            if(value)
            {
                foreach (ParticleSystem engine in engines)
                    engine.Play();
            }
            else
            {
                foreach (ParticleSystem engine in engines)
                    engine.Stop();
            }
            engine = value;
        }
    }
    protected List<Transform> lasers = new List<Transform>();

    protected Vector3 NewPosition;

    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public bool Attack = false;
    [HideInInspector]
    public float LastAttack = 0;

    protected float RotateAngle = 0;



    protected virtual void Start()
    {
        NewPosition = transform.position;

        foreach (Transform engine in EngineTransform)
            engines.Add(engine.GetComponent<ParticleSystem>());
        foreach (Transform laser in LaserTransform)
            lasers.Add(laser);

        ShipNameGameObject.GetComponent<TextMesh>().text = Name + " [" + Level + "]";

        engine = true;
        Engines = false;

        foreach (LaserBullets laser in AmmunitionsLaser)
        {
            Ammunition ammunition = new Ammunition();
            ammunition.Bullet = GameData.LaserObjects[laser];
            ammunition.TimeReload = GameData.LaserObjects[laser].GetComponent<Bullet>().TimeReload;
            Ammunitions.Add(ammunition);
        }

        foreach (RocketBullets rocket in AmmunitionsRocket)
        {
            Ammunition ammunition = new Ammunition();
            ammunition.Bullet = GameData.RocketObjects[rocket];
            ammunition.TimeReload = GameData.RocketObjects[rocket].GetComponent<Bullet>().TimeReload;
            Ammunitions.Add(ammunition);
        }
    }

    protected virtual void Update()
    {
        Repair();
        ShotFire();
    }

    protected abstract void ShotFire();
    
    protected void CreateBullet(int damage, Ammunition ammunition, List<Transform> trans)
    {
        foreach(Transform t in trans)
        {
            GameObject bullet = Instantiate(ammunition.Bullet, t.position, Quaternion.identity);
            Bullet bulletscript = bullet.GetComponent<Bullet>();
            bulletscript.Target = Target;
        }

        GameObject go = Instantiate(ammunition.DamageMessagePrefab, Target.transform.position, Quaternion.identity);
        if (damage > 0)
            go.GetComponent<TextMesh>().text = damage.ToString();

        Target.GetComponent<Ship>().TakeDamage(damage, this);
        LastAttack = 0;
    }

    public void TakeDamage(int damage, Ship ship)
    {
        if(damage < 1)
        {
            return;
        }
        LastAttack = 0;

        int inHp = (int)(damage * 0.7f);
        int inShd = damage - inHp;
        if (Shields > 0)
        {
            if (Shields >= inShd)
            {
                Shields -= inShd;
            }
            else
            {
                int inhp = inShd - Shields;
                Shields -= Shields;
                inHp += inhp;
            }
        }
        else
        {
            inHp = damage;
            inShd = 0;
        }

        if (inHp > 0)
        {
            if (Hitpoints - inHp > 0)
                Hitpoints -= inHp;
            else
                Destroy();
        }
    }

    protected abstract void Destroy();

    protected virtual void Repair()
    {
        if (Attack)
            return;

        if(LastAttack < GameData.RepairTime)
        {
            LastAttack += Time.deltaTime;
            return;
        }

        // Repair
    }

    protected virtual void Fly(GameObject model)
    {
        if (NewPosition != model.transform.position)
        {
            model.transform.position = Vector3.MoveTowards(model.transform.position, NewPosition, Time.deltaTime * Speed / GameData.FlySpeed);
            if (!engine)
                Engines = true;
        }
        else if (engine)
            Engines = false;
    }

    protected virtual void Rotate(GameObject model)
    {
        if (Target != null && Attack)
            RotateAngle = Mathf.Atan2(Target.transform.position.y - model.transform.position.y, Target.transform.position.x - model.transform.position.x) * Mathf.Rad2Deg + 90;
        else
        {
            float angle = Mathf.Atan2(NewPosition.y - model.transform.position.y, NewPosition.x - model.transform.position.x);
            if (angle == 0)
                return;
            RotateAngle = angle * Mathf.Rad2Deg + 90;
        }

        ShipGameObject.transform.rotation = Quaternion.Lerp(ShipGameObject.transform.rotation, Quaternion.Euler(0, 0, RotateAngle), Time.deltaTime * GameData.RotateSpeed);
    }
}