﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : ProjectileAbility
{
    public GameObject bullet;
    public Transform nozzle;

    public void Start()
    {
        base.Start();
        bullet = projectile.gameObject;
    }
    public override void Fire()
    {
        Debug.Log("Firing");
        ProjectileBehavior bulletInstance = Instantiate(bullet, nozzle.position, transform.rotation).GetComponent<ProjectileBehavior>();
        // bullet can only do damage on the server
        //bulletInstance.canDoDamage = bPlayer.isServer;
    }

    public override void Release()
    {

    }
}
