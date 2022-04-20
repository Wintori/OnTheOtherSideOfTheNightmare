using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStartState : MonoBehaviour
{
    [SerializeField] private bool startOpen;
    

    private void Start()
    {
        DoorAnims doorAnims = GetComponent<DoorAnims>();
        if (startOpen)
        {
            doorAnims.OpenDoor();
        }
        else
        {
            doorAnims.CloseDoor();
        }

        
    }
}
