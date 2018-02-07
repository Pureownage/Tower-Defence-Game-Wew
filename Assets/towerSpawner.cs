using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerSpawner : MonoBehaviour {

    public GameObject Tower;

    // Quick and dirty tower spawner
    // This will need to change, perhaps take in a set tower type?
    private void OnMouseDown()
    {
        Instantiate(Tower,this.transform.position,this.transform.rotation);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
