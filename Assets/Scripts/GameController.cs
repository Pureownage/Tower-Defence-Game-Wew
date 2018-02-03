using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public int money = 100;
    private int lives = 20;
    bool StopTime = false;


    public Text moneyText;
    public Text liveText;
    // Get and setters, so we don't need to do everything every frame, instead only when it needs to be updated.
    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
            liveText.text = "Lives :" + lives.ToString();
        }
    }

    // Use this for initialization
    void Start () {
        moneyText.text = "Money : " + money.ToString();
        liveText.text = "Lives :" + lives.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //  Does the player want to stop time? Lets Find out
        // Here, wewant to see if the player pressed space bar to stop time or resume time
        if (Input.GetKeyDown("space"))
        {
            //If they did, we find out if they are stopped or not.
            // If StopTime == false, it means that time is not currently stopped, so we set the time scale to 0, which causes the delta time to become 0
            if(StopTime == false)
            {
                Time.timeScale = 0f;
                StopTime = true;
                print("Time should stop here");
            }
            // Otherwise, we set the timescale to 1, meaning that the player wants to resume time and we change the delta time to 1.
            else
            {
                Time.timeScale = 1f;
                StopTime = false;
                print("Time should start here");            }
        }
	}
}
