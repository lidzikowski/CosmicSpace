using System;
using System.Collections.Generic;
using UnityEngine;

public enum LaserBullets {
    Laser0, Laser1, Laser2, Laser3, Laser4, Laser5, Laser6, Laser7
}
public enum RocketBullets {
    Rocket0, Rocket1, Rocket2, Rocket3
}

public abstract class Bullet : MonoBehaviour
{
    [Header("Properties")]
    public string Name;
    [Range(0.5f, 10f)]
    public float Multiplier;
    [Range(1, 100)]
    public int Precision;
    [Range(0.5f, 30f)]
    public float TimeReload;
    
    [Header("Damage Message")]
    public GameObject DamageMessagePrefab;

    [HideInInspector]
    public GameObject Target;

    protected float RotateAngle = 0;
    protected Vector3 TargetPosition;
    protected float Timer = 0;

    protected virtual void Start()
    {
        float angle = Mathf.Atan2(Target.transform.position.y - transform.position.y, Target.transform.position.x - transform.position.x);
        if (angle == 0)
            return;
        RotateAngle = angle * Mathf.Rad2Deg + 90;

        TargetPosition = Target.transform.position;

        GetComponent<AudioSource>().Play();
    }

    protected virtual void LateUpdate()
    {
        Timer += Time.deltaTime / 0.4f;

        Fly();
        Rotate();
        Explosion();
    }

    protected virtual void Explosion()
    {
        if (Vector2.Distance(transform.position, TargetPosition) < 1)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, RotateAngle), Time.deltaTime * GameData.RotateSpeed * 10);
    }

    protected virtual void Fly()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Timer);
    }
}