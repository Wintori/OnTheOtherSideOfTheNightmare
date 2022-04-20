using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Npc : Collidable
{
    //public DialogueTrigger trigger;

    //private float cooldown = 9999.0f;
    //private float lastShout;

    //protected override void Start()
    //{
    //    base.Start();
    //    lastShout = -cooldown;
    //}

    //protected override void OnCollide(Collider2D coll)
    //{
    //    if (Time.time - lastShout > cooldown)
    //    {
    //        lastShout = Time.time;
    //        if (coll.name == "Player")
    //            trigger.StartDialogue();
    //    }
    //}

    //ffffffffff

    public NPCConversation myConversation;

    public NPCConversation myConversation1;

    public bool isAlreadyTalked = false;


    protected override void OnCollide(Collider2D coll)
    {

       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConversationManager.Instance.EndConversation();
        }


        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            return;


            if (coll.name != "Player")
            return;

        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isAlreadyTalked == false)
            {
                ConversationManager.Instance.StartConversation(myConversation);
                isAlreadyTalked = true;
            }
            else
            {
                ConversationManager.Instance.StartConversation(myConversation1);
            }
        }
    }
    



}
