using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCollision : MonoBehaviour {
    BeeController beeController;
    BeeStatus beeInventory;

    public int totalPollen = 10;
    public int pollenPerGrab = 2;
    public float bounceForce = 1.0f;

    void Start()
    {
        beeController = GetComponent<BeeController>();
        beeInventory = GetComponent<BeeStatus>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        beeController.Bounce(bounceForce);
        if (totalPollen > 0) {
            beeInventory.pollenCollected += pollenPerGrab;
            totalPollen -= pollenPerGrab;
        }
        Debug.Log("New Pollen Count: " + beeInventory.pollenCollected.ToString());
    }
}
