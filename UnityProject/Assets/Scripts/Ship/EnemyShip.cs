using UnityEngine;

public abstract class EnemyShip : Ship
{
    [Header("Properties")]
    public int Damage;
    [Range(20, 35)]
    public int ShotRange;
    public bool Aggresive;
    public Reward Reward;
    
    protected Vector3 LastTargetPosition;
    protected float UpdateTimer = 0;



    protected override void Start()
    {
        base.Start();

        Hitpoints = MaxHitpoints;
        Shields = MaxShields;
    }

    protected override void Update()
    {
        base.Update();

        Fly(gameObject);
        Rotate(gameObject);

        if (UpdateTimer > 1)
        {
            FindTarget();
            FindPosition();
            UpdateTimer = 0;
        }
        else
            UpdateTimer += Time.deltaTime;
    }


    
    protected virtual void FindTarget()
    {
        if (Target == null)
        {
            if (Aggresive && Vector3.Distance(GameData.LocalPlayer.transform.position, transform.position) < ShotRange)
            {
                Target = GameData.LocalPlayer.Ship.gameObject;
                Attack = true;
            }
        }
        else
        {
            if (Vector3.Distance(Target.transform.position, transform.position) > 50)
            {
                Attack = false;
                Target = null;
                Debug.Log(name + " zgubil target");
            }
        }
    }

    protected virtual void FindPosition()
    {
        if(Target != null)
        {
            if(LastTargetPosition != Target.transform.position)
            {
                LastTargetPosition = Target.transform.position;
                NewPosition = Game.RandomCircle(Target.transform.position, Random.Range(10, 15));
            }
        }
        else
        {
            if(Random.Range(0, 100) < 50)
            {
                NewPosition = Game.RandomCircle(transform.position, Random.Range(15, 50));
            }
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
            foreach (Ammunition ammunition in Ammunitions)
            {
                if (ammunition.Ready)
                {
                    if (Vector3.Distance(transform.position, Target.transform.position) < ShotRange)
                    {
                        CreateBullet(Game.Damage(Damage, ammunition), ammunition, lasers);
                    }
                }
            }
        }
    }

    protected override void Destroy()
    {
        if (Reward != null)
            GameData.LocalPlayer.ApplyReward(Reward);

        Destroy(gameObject);
    }
}