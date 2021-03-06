﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : BasePlayer
{
    public Sprite hookerSprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        movementSpeed = 5;
        gameObject.layer = 9; // Hooker
        base.Start();

        foreach(Ability ability in abilities)
        {
            ability.gameObject.SetActive(true);
            ability.transform.parent = null;
            ability.gameObject.GetComponent<HookUtils.Hook>().SetPlayer(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
