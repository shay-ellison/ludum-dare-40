using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    public Camera mainCamera;
    public Rigidbody2D beeRigidbody;

    public float gravityScale = 0.25f;
    public float flyForce = 5.0f;
    public float flyTime = 0.05f;

    private Bee bee;
    private float minDistanceFromTop = 1.0f;
    // private float screenQuadWidth;

    public bool flying = false;
    public bool onGround = false;
    public bool treeCollision = false;

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

        bee = GetComponent<Bee>();

        // Vector3 screenUpperCorner = new Vector3(Screen.width, Screen.height, 0f);
        // Vector3 worldUpperCorner = mainCamera.ScreenToWorldPoint(screenUpperCorner);
        // screenQuadWidth = worldUpperCorner.x;

        Vector3 top = new Vector3(0f, Screen.height, 0f);
        worldTop = mainCamera.ScreenToWorldPoint(top);
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {
        
        float gravityForce = Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;

        switch (bee.getCurrentBodyState())
        {
            case Bee.BodyState.Normal:
                FixedUpdateNormal(gravityForce);
                break;
            case Bee.BodyState.Fat:
                FixedUpdateNormal(gravityForce);
                break;
            case Bee.BodyState.Obese:
                FixedUpdateNormal(gravityForce);
                break;
            default:
                break;
        }

        // Debug.Log(Physics2D.gravity.y);
        // Debug.Log(gravityForce);

        /*
          if (Input.GetButtonDown("Dash"))
            {
                Debug.Log("Dash");
                _velocity += Vector3.Scale(transform.forward, 
                                           DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 
                                                                      0, 
                                                                      (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
            }
         */
    }

    private void FixedUpdateNormal(float gravityForce)
    {
        float upForce = 0.0f;
        float horizontalMoveForce = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            float distanceFromTop = worldTop.y - beeRigidbody.position.y;
            // Debug.Log("Distance: " + distanceFromTop.ToString());
            if (distanceFromTop <= minDistanceFromTop)
            {
                // Debug.Log("Less than Distance " + minDistanceFromTop.ToString());
                upForce = -gravityForce;  // enough to cancel gravity out
            }
            else
            {
                upForce += flyForce * Time.deltaTime;
                // upForce = Mathf.SmoothDamp()
                // Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
        }

        // Debug.Log("UpForce = " + upForce.ToString());

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMoveForce = -3.0f * Time.fixedDeltaTime;
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);  // flip it left
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMoveForce = 3.0f * Time.fixedDeltaTime;
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);  // flip it right
        }

        // Gravity
        if (!onGround)
        {
            Vector2 gravityVector = new Vector2(Physics.gravity.x, gravityForce);
            beeRigidbody.position += gravityVector;
        }

        // Input Response
        // Fly
        Vector2 flyVector = new Vector2(0f, upForce);
        beeRigidbody.position += flyVector;

        if (upForce > 0)
        {
            flying = true;
            onGround = false;  // might be on ground until you apply an upForce of some sort
        }
        else
        {
            flying = false;
        }

        if (!onGround && !treeCollision)
        {
            // Move
            Vector2 horizontalVector = new Vector2(horizontalMoveForce, 0f);
            beeRigidbody.position += horizontalVector;
        }
    }

    private void FixedUpdateFat(float gravityForce)
    {

    }

    private void FixedUpdateObese(float gravityForce)
    {

    }
}
