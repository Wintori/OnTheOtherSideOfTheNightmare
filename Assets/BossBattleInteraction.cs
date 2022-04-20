using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleInteraction : MonoBehaviour
{
    [SerializeField] private DoorAnims entryDoor;
    [SerializeField] private BossBattle bossBattle;

    private void Start()
    {
        bossBattle.OnBossBattleStarted += BossBattle_OnBossBattleStarted;
        bossBattle.OnBossBattleOver += BossBattle_OnBossBattleOver;
    }

    private void BossBattle_OnBossBattleOver(object sender, System.EventArgs e)
    {
        entryDoor.OpenDoor();
    }

    private void BossBattle_OnBossBattleStarted(object sender, System.EventArgs e)
    {
        entryDoor.CloseDoor();
    }
}

