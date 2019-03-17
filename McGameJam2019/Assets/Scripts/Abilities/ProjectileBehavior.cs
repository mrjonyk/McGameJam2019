﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    protected float birthTime;
    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
        Destroy(this.gameObject, 10);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = (transform.forward * Time.deltaTime * 0.5f);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Translate(transform.forward * Time.deltaTime * 0.5f);
    //    if(Time.time - birthTime > 10)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject obj = collision.gameObject;
            if(obj.tag == "Player")
            {
                Hooker hook = obj.GetComponent<Hooker>();
                if(hook != null)
                {
                    hook.Damage(5);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}