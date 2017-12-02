using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour {
    BeeController beeController;
    Bee bee;

    void Start() {
        beeController = GetComponent<BeeController>();
        bee = GetComponent<Bee>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject otherGameObject = other.gameObject;

        Debug.Log(otherGameObject.name);

        if (otherGameObject.name == "Ground")
        {
            Debug.Log("Ground!");
            beeController.onGround = true;
        }
        else if (otherGameObject.name == "Sunflower")  // Will basically do this for any flower
        {
            Flower flower = otherGameObject.GetComponent<Flower>();
            bee.pollenCollected += flower.GimmePollen();
            // beeController.Bounce(bounceForce);
        }
    }
}
