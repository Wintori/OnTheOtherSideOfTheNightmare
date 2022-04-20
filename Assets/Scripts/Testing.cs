using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private DoorAnims entryDoor;
    [SerializeField] private DoorAnims exitDoor;
    [SerializeField] private BattleSystem battleSystem;

    private void Start()
    {
        battleSystem.OnBattleStarted += BattleSystem_OnBattleStarted;
        battleSystem.OnBattleOver += BattleSystem_OnBattleOver;
    }

    private void BattleSystem_OnBattleOver(object sender, System.EventArgs e)
    {
        exitDoor.OpenDoor();
        entryDoor.OpenDoor();

    }

    private void BattleSystem_OnBattleStarted(object sender, System.EventArgs e)
    {
        entryDoor.CloseDoor();
        
    }
}
