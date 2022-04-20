using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class SpawnSystem : MonoBehaviour
{
    public GameObject enemyPerfab;

    private Enemy enemyMain;

   
   
    
    

   


    private void Awake()
    {
        gameObject.SetActive(false);
        enemyMain = GetComponent<Enemy>();
    }

    
    public void Spawn()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    public bool IsAlive()
    {
        return !enemyMain.IsDead;
    }

    public void KillEnemy()
    {

        Destroy(gameObject);
    }

}
