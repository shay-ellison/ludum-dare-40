using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public int totalPollen = 10;
    private int pollenPerGrab = 2;

    public float bounceForce = 1.0f;

    public int GimmePollen()
    {
        if (totalPollen > 0)  // Give pollen if there's any to give
        {
            totalPollen -= pollenPerGrab;
            return pollenPerGrab;
        } else
        {
            return 0;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
