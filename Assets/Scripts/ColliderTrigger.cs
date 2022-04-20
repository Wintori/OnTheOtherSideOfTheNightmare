using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : Collidable
{

    public event EventHandler OnPlayerEnterTrigger;
    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name != "Player")
            return;


        Debug.Log("Player inside trigger!");
        OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
    }
}
