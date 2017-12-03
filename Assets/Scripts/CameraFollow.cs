using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;

    public GameObject bee;

    public Vector2 minCameraPosition;
    public Vector2 maxCameraPosition;

	// Use this for initialization
	void Start () {
        bee = GameObject.FindGameObjectWithTag("Player");
	}

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, bee.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, bee.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        // Camera Bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
            Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
            transform.position.z);   
    }
}
