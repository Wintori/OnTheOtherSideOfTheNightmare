using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using QFSW.QC;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    public Animator myAnim;
    private Transform target;
    Vector2 movement;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;


        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }

    private void FixedUpdate()
    {

        if (FindObjectOfType<QuantumConsole>().IsActive)
            return;

        if (movement.sqrMagnitude != 0)
        {
            SoundManageer.PlaySound(SoundManageer.Sound.PlayerMove);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        myAnim.SetFloat("Speed", movement.sqrMagnitude);

        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            return;

        

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            
            UpdateMorot(new Vector3(x, y, 0));
            myAnim.SetFloat("Horizontal", 1);
        }
    }

    public void SwapSprite(int skinid)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinid];
    }

    public void OnLevelUp()
    {
        maxHitpoing++;
        hitpoint = maxHitpoing;
    }
    [Command]
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }


    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoing)
            return;

        hitpoint += healingAmount;

        if (hitpoint > maxHitpoing)
            hitpoint = maxHitpoing;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    public void Respawn()
    {
        Heal(maxHitpoing);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }


    private void RestoreAllHP()
    {
       
    }

}
