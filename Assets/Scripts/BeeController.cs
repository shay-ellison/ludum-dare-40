using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    public Camera mainCamera;
    public Rigidbody2D beeRigidbody;

    public float gravityScale = 0.25f;
    private float fatGravityScale = 1.5f;
    private float obeseGravityScale = 2f;

    public float flyForce = 5.0f;  // needs to beat gravity, TODO: calculate based on gravity?

    private Bee bee;
    private float minDistanceFromTop = 1.0f;
    // private float screenQuadWidth;

    // Position/Collision Controls - TODO: whole system could be better :) -- Flags
    public bool flying = false;
    public bool onGround = false;
    public bool onPlatform = false;
    public bool treeCollision = false;
    public bool jumping = false;

    private float bounceForceGoal = 0.0f;

    private Vector3 worldTop;
    private Vector2 jumpVelocity;

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
        switch (bee.getCurrentBodyState())
        {
            case Bee.BodyState.Fat:
               UpdateFat();
                break;
            case Bee.BodyState.Obese:
                UpdateObese();
                break;
            default:
                break;
        }
    }

    private void UpdateFat()
    {
        float jumpHeight = 2.0f;
        float moveSpeed = 0.1f;
        float horizontalX = 0.0f;

        if (Input.GetKey(KeyCode.RightArrow))  // Move right
        {
            horizontalX = moveSpeed;
            LookRight();
        } else if (Input.GetKey(KeyCode.LeftArrow))  // Move left
        {
            horizontalX = -moveSpeed;
            LookLeft();
        }

        if (Input.GetKeyDown(KeyCode.Space))  // Jump, now
        {
            if (onGround || onPlatform)
            {
                Vector2 jumpVector = new Vector2(0, jumpHeight);
                beeRigidbody.position += jumpVector;

                onGround = false;
                onPlatform = false;
                jumping = true;
            }
        }

        // Move
        if (!treeCollision && (jumping || onGround || onPlatform))
        {
            Vector2 horizontalVector = new Vector2(horizontalX, 0);
            beeRigidbody.position += horizontalVector;
        }

        if (onGround || onPlatform)
        {
            jumping = false;
        }
    }

    private void UpdateObese()
    {
        float jumpHeight = 2.0f;
        float moveSpeed = 0.05f;
        float horizontalX = 0.0f;

        if (Input.GetKey(KeyCode.RightArrow))  // Move right
        {
            horizontalX = moveSpeed;
            LookRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))  // Move left
        {
            horizontalX = -moveSpeed;
            LookLeft();
        }

        if (Input.GetKeyDown(KeyCode.Space))  // Jump, now
        {
            if (onPlatform)  // Can only Jump on platform if obese
            {
                Vector2 jumpVector = new Vector2(0, jumpHeight);
                beeRigidbody.position += jumpVector;

                onGround = false;
                onPlatform = false;
                jumping = true;
            }
        }

        // Move
        if (!treeCollision && (jumping || onGround || onPlatform))
        {
            Vector2 horizontalVector = new Vector2(horizontalX, 0);
            beeRigidbody.position += horizontalVector;
        }

        if (onGround || onPlatform)
        {
            jumping = false;
        }
    }

    void FixedUpdate() {
        // Standard Gravity Force
        float gravityForce = Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;

        switch (bee.getCurrentBodyState())
        {
            case Bee.BodyState.Normal:
                FixedUpdateNormal(gravityForce);
                break;
            case Bee.BodyState.Fat:
                FixedUpdateFat(gravityForce);
                break;
            case Bee.BodyState.Obese:
                FixedUpdateObese(gravityForce);
                break;
            default:
                break;
        }
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
                upForce += flyForce * Time.fixedDeltaTime;
                // upForce = Mathf.SmoothDamp()
                // Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
        }

        // Debug.Log("UpForce = " + upForce.ToString());

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMoveForce = -3.0f * Time.fixedDeltaTime;
            LookLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMoveForce = 3.0f * Time.fixedDeltaTime;
            LookRight();
        }

        // Gravity
        ApplyGravity(gravityForce);

        // Fly
        Vector2 flyVector = new Vector2(0f, upForce);
        beeRigidbody.position += flyVector;

        // Update Position Attributes as far as we know
        if (upForce > 0)
        {
            flying = true;
            onGround = false;  // might be on ground until you apply an upForce of some sort
        }
        else
        {
            flying = false;
        }

        // Move
        if (!onGround && !treeCollision)
        {
            Vector2 horizontalVector = new Vector2(horizontalMoveForce, 0f);
            beeRigidbody.position += horizontalVector;
        }
    }

    private void FixedUpdateFat(float gravityForce)
    {
        // Only Gravity Happens here now
        // gravityForce *= fatGravityScale;
        ApplyGravity(gravityForce);

    }

    private void FixedUpdateObese(float gravityForce)
    {
        // Only Gravity Happens here now
        // gravityForce *= obeseGravityScale;
        ApplyGravity(gravityForce);
    }

    private void ApplyGravity(float gravityForce)
    {
        // Gravity
        if (!onGround && !onPlatform)
        {
            Vector2 gravityVector = new Vector2(Physics.gravity.x, gravityForce);
            beeRigidbody.position += gravityVector;
        }
    }

    private void LookLeft()
    {
        transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);  // flip it left
    }

    private void LookRight()
    {
        transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);  // flip it right
    }
}
