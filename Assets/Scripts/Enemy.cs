using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using QFSW.QC;
using System;



public class Enemy : Mover
{
    public int xpValue = 1;

    public float triggerLenght = 1;

    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    private BoxCollider2D hitbox;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];

    private Animator myAnim;
    private Transform target;
    public int DeathTime;

    public bool IsDead = false;

    public event EventHandler OnDead;

    protected override void Start()
    {
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
        myAnim.SetBool("isMoving", false);

        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;
            if (chasing)
            {
                myAnim.SetBool("isMoving", true);
                myAnim.SetFloat("moveX", 1);


                if (!collidingWithPlayer)
                {
                    UpdateMorot((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMorot(startingPosition - transform.position);
            }

        }
        else
        {
            UpdateMorot(startingPosition - transform.position);
            chasing = false;
        }
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
                myAnim.SetBool("isAttack", true);
            }
            // The array is not cleaned up, so we it ourself
            myAnim.SetBool("isAttack", false);
            hits[i] = null;

        }
    }


   

    public float GetHealthPecent()
    {
        return (float)hitpoint / (float)maxHitpoing;
    }

    protected override void Death()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
        IsDead = !IsDead;
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        SoundManageer.PlaySound(SoundManageer.Sound.EnemyDie);
    }
}
