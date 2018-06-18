using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    private float timer = 0;

    void LateUpdate()
    {
        if (timer > 0.5f)
            Destroy(gameObject);
        else
        {
            timer += Time.deltaTime;
            transform.position += new Vector3(0.05f, 0.1f);
        }
    }
}