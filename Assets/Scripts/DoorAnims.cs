using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnims : MonoBehaviour
{
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool("Open", false);
    }
}
