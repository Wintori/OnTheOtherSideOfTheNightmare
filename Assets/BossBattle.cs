using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class BossBattle : MonoBehaviour
{

    public event EventHandler OnBossBattleStarted;
    public event EventHandler OnBossBattleOver;

    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private SpawnSystem pfEnemySpawn;

    

    private Stage stage;

    private List<Vector3> spawnPositionList;

    public Enemy enemy;

    private void Awake()
    {
        spawnPositionList = new List<Vector3>();
        foreach (Transform spawnPosition in transform.Find("spawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }

        stage = Stage.WaitingToStart;
        

    }

    

    private void Start()
    {
        
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        enemy.OnDamaged += BossBattle_OnDamaged;
        enemy.OnDead += BossBattle_OnDead;
    }

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        // Turret dead! Boss battle over!
        Debug.Log("Boss Battle Over!");
        FunctionPeriodic.StopAllFunc("spawnEnemy");

        OnBossBattleOver?.Invoke(this, EventArgs.Empty);
        
    }

    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        //Turret took damage

        switch (stage)
        {
            
            case Stage.Stage_1:
                if (enemy.GetHealthPecent() <= .7f)
                {
                    //Turert under 70% health
                    StartNextStage();
                }
                break;

            case Stage.Stage_2:
                if (enemy.GetHealthPecent() <= .3f)
                {
                    //Turert under 30% health
                    StartNextStage();
                }
                break;

            
        }
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        
    }

    private void StartBattle()
    {
        Debug.Log("StartBatlle");
        StartNextStage();
        SpawnEnemy();
        FunctionPeriodic.Create(SpawnEnemy, 4f, "spawnEnemy");

        OnBossBattleStarted?.Invoke(this, EventArgs.Empty);
    }

    private void StartNextStage()
    {
        switch (stage)
        {
            
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = spawnPositionList[UnityEngine.Random.Range(0, spawnPositionList.Count)];
        SpawnSystem enemySpawn = Instantiate(pfEnemySpawn, spawnPosition, Quaternion.identity);
        enemySpawn.Spawn();


        
    }


    
}
