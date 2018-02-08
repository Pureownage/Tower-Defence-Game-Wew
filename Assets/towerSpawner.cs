using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour {

    GameObject gameManager;
    public GameObject Tower;

    // TO DO: Alot of this is going to change to implement different types of tower. Most likely a ui drop down which will trigger what to spawn.

    // Quick and dirty tower spawner
    // This will need to change, perhaps take in a set tower type?
    private void OnMouseDown()
    {
        gameManager = GameObject.Find("_Scripts");
        if(gameManager.GetComponent<GameController>().BuyTower(100) == true)
        {
            Instantiate(Tower, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            // TO DO: I Need to implement a message where the player is informed they cannot pruchase as they do not have enough money.
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
