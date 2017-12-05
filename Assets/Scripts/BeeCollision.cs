using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour {
    private BeeController beeController;
    private Bee bee;
    private BeeSoundPlayer soundPlayer;

    void Start() {
        beeController = GetComponent<BeeController>();
        bee = GetComponent<Bee>();
        soundPlayer = GetComponent<BeeSoundPlayer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.tag == "Flower")
        {  
            Flower flower = otherGameObject.GetComponent<Flower>();
            int pollen = flower.GimmePollen();
            if (pollen > 0)
            {
                bee.pollenCollected += pollen;
                soundPlayer.PlayGrabPollen();
            }
        }
        else if (otherGameObject.tag == "DeathFlower")
        {
            DeathFlower deathFlower = otherGameObject.GetComponent<DeathFlower>();
            int deathPollen = deathFlower.GimmeDeathPollen();
            if (deathPollen > 0)
            {
                bee.deathPollenCollected += deathPollen;
                soundPlayer.PlayGrabDeathPollen();
            }
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
