using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Weapon : Collidable
{
    // Damage struct

    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f,4f };

    //Upgrade

    public int weaponLeve = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
                SoundManageer.PlaySound(SoundManageer.Sound.PlayerAttack);
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
       if(coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            // Create a new damage obj

            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLeve],
                origin = transform.position,
                pushForce = pushForce[weaponLeve]
            };

            coll.SendMessage("ReceiveDamage", dmg);

        }
    }

    private void Swing()
    {
        anim.SetTrigger("SWING");
        Debug.Log("Swing");
    }
    public void UpgradeWeapon()
    {
        weaponLeve++;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLeve];
    }
    public void SetWeaponLevel(int level)
    {
        weaponLeve=level;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLeve];
    }
}
