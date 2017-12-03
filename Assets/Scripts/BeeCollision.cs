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

        if (otherGameObject.tag == "Flower")  // NOTE: A REAL TRIGGER! =)
        {  // Will basically do this for any flower
            Flower flower = otherGameObject.GetComponent<Flower>();
            bee.pollenCollected += flower.GimmePollen();
            // beeController.Bounce(bounceForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.name == "Ground")
        {
            beeController.onGround = true;
        }
        else if (otherGameObject.tag == "Leaf")
        {
            beeController.onPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.tag == "Leaf")
        {
            beeController.onPlatform = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.name == "Ground")
        {
            beeController.onGround = false;
        }
        else if (otherGameObject.tag == "Leaf")
        {
            beeController.onPlatform = false;
        }
    }
}
