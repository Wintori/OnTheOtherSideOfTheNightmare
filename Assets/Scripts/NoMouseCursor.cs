using UnityEngine;
using System.Collections;
using DialogueEditor;

public class NoMouseCursor : MonoBehaviour
{
    public bool lockCursor = true;



    // Use this for initialization
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        HideCursor();
    }

    public void HideCursor()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

}