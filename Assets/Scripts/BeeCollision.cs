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

        // Debug.Log(otherGameObject.name);

        if (otherGameObject.name == "Ground")
        {
            beeController.onGround = true;
        }
        else if (otherGameObject.tag == "Flower")
        {  // Will basically do this for any flower
            Flower flower = otherGameObject.GetComponent<Flower>();
            bee.pollenCollected += flower.GimmePollen();
            // beeController.Bounce(bounceForce);
        }
        else if (otherGameObject.tag == "Tree")
        {
            beeController.treeCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.tag == "Tree")
        {
            beeController.treeCollision = false;
        }
    }
}
