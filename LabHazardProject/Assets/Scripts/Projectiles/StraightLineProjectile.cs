using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineProjectile : Projectile
{
    [SerializeField]
    private float speed = 2f;

    public override void Move()
    {
        rb.velocity = travelDirection * speed;
    }
}
