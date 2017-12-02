using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    public Camera mainCamera;
    public Rigidbody2D beeRigidbody;

    public float gravityScale = 0.25f;
    public float flyForce = 5.0f;

    private float minDistanceFromTop = 1.0f;
    private float screenQuadWidth;

    private bool flying = false;

    private float bounceForceGoal = 0.0f;

    private Vector3 worldTop;

    public void Bounce(float bounceForce) {
        bounceForceGoal += bounceForce;
    }

    // Use this for initialization
    void Start() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }

        if (beeRigidbody == null) {
            beeRigidbody = GetComponent<Rigidbody2D>();
        }

        Vector3 screenUpperCorner = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 worldUpperCorner = mainCamera.ScreenToWorldPoint(screenUpperCorner);
        screenQuadWidth = worldUpperCorner.x;

        Vector3 top = new Vector3(0f, Screen.height, 0f);
        worldTop = mainCamera.ScreenToWorldPoint(top);
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {
        float upForce = 0.0f;
        float horizontalMoveForce = 0.0f;
        float gravityForce = Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;

        // Debug.Log(Physics2D.gravity.y);
        // Debug.Log(gravityForce);

        if (Input.GetKey(KeyCode.UpArrow)) {
            flying = true;

            float distanceFromTop = worldTop.y - beeRigidbody.position.y;
            // Debug.Log("Distance: " + distanceFromTop.ToString());
            if (distanceFromTop <= minDistanceFromTop) {
                // Debug.Log("Less than Distance " + minDistanceFromTop.ToString());
                upForce = -gravityForce;  // enough to cancel gravity out
            } else {
                upForce += flyForce * Time.deltaTime;
            }
        } else {
            flying = false;
        }

        // Debug.Log("UpForce = " + upForce.ToString());

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontalMoveForce = -3.0f * Time.fixedDeltaTime;
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);  // flip it left
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            horizontalMoveForce = 3.0f * Time.fixedDeltaTime;
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);  // flip it right
        }

        // Gravity
        Vector2 gravityVector = new Vector2(Physics.gravity.x, gravityForce);
        beeRigidbody.position += gravityVector;

        // Input Response
        // Fly
        Vector2 flyVector = new Vector2(0f, upForce);
        beeRigidbody.position += flyVector;

        // Move
        Vector2 horizontalVector = new Vector2(horizontalMoveForce, 0f);
        beeRigidbody.position += horizontalVector;
    }
}
