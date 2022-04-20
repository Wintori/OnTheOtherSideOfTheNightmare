using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSystem : MonoBehaviour
{

    public event EventHandler OnBattleStarted;
    public event EventHandler OnBattleOver;


    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Wave[] waveArray;

    private enum State
    {
        Idle,
        Active,
        BattleOver,
    }



    private State state;

    private void Awake()
    {
        state = State.Idle;
    }
    void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        if (state == State.Idle)
        {
            StartBattle();
            colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
        }
       
    }

    private void StartBattle()
    {
        Debug.Log("StartBattle");
        state = State.Active;
        OnBattleStarted?.Invoke(this, EventArgs.Empty);
    }


    private void Update()
    {
        switch (state) 
        {
            case State.Active:
                foreach (Wave wave in waveArray)
                {
                wave.Update();
                }
                TestBattleOver();
                break;
        }
    }

    private void TestBattleOver()
    {
        if (state == State.Active)
        {
            if (AreWavesOver())
            {
                // Battle is over!
                state = State.BattleOver;
                Debug.Log("Battle is over!");
                OnBattleOver?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool AreWavesOver()
    {
        foreach (Wave wave in waveArray)
        {
            if (wave.IsWaveOver())
            {
                // Wave is over
            }
            else
            {
                //Wave not ove
                return false;
            }
        }
        return true;
    }




    /*
     * Represents a single Enemy Spawn Wave
     */

    [System.Serializable]
    private class Wave
    {
        [SerializeField] private SpawnSystem[] enemySpawnArray;
        [SerializeField] private float timer;

        public void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SpawnEnemies();
                }
            }
        }
        private void SpawnEnemies()
        {
            foreach (SpawnSystem enemySpawn in enemySpawnArray)
            {
                enemySpawn.Spawn();
            }
        }


        public bool IsWaveOver()
        {
            if (timer < 0)
            {
                // Wave spawned
                foreach (SpawnSystem enemySpawn in enemySpawnArray)
                {
                    if (enemySpawn.IsAlive())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                // Enemies haven't spawned yet
                return false;
            }
        }


    }




}
