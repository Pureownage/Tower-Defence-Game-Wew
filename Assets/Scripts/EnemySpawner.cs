using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyToSpawn;
    public int numberToSpawn;
    public float spawnCountdown = 1;
    public bool spawnEnemys = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(numberToSpawn <= 0)
        {
            spawnEnemys = false;
            numberToSpawn = 5;
        }
	    if (spawnEnemys == true)
        {
            if(spawnCountdown <= 0)
            {
                Instantiate(enemyToSpawn,this.transform.position,this.transform.rotation);
                spawnCountdown = 1;
                numberToSpawn--;
            } else
            {
                spawnCountdown -= Time.deltaTime;
            }
        }	
	}


    public void TriggerSpawn()
    {
        spawnEnemys = true;
    }
}
