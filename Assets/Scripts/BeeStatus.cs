using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStatus : MonoBehaviour {
    public enum BeeState { Normal, Fat, Obese };
    
    public int pollenGoal = 10;  // set at level start
    public int pollenCollected = 0;

    private BeeState currentState;

    public BeeState getCurrentState() {
        return currentState;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void updateState(BeeState newState) {

    }
}
