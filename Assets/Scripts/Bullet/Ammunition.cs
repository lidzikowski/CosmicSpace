using UnityEngine;

public class Ammunition
{
    public GameObject Bullet;
    public float TimeReload;
    public float Multiplier
    {
        get
        {
            return Bullet.GetComponent<Bullet>().Multiplier;
        }
    }
    public int Precision
    {
        get
        {
            return Bullet.GetComponent<Bullet>().Precision;
        }
    }
    public GameObject DamageMessagePrefab
    {
        get
        {
            return Bullet.GetComponent<Bullet>().DamageMessagePrefab;
        }
    }


    private float timer = 5;
    public bool Ready
    {
        get
        {
            if (timer >= TimeReload)
            {
                timer = 0;
                return true;
            }
            else
            {
                timer += Time.deltaTime;
                return false;
            }
        }
    }
    public bool CanShot(int count, int storage = -1)
    {
        if (storage == -1)
            return true;

        if (storage >= count)
        {
            storage -= count;
            return true;
        }
        else
            return false;
    }
}