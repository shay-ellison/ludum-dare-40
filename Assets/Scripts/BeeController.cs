using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    public Camera mainCamera;
    private Rigidbody2D beeRigidbody;
    private SpriteRenderer beeSpriteRenderer;

    private float flyForce = 1.0f;

    // Position/Collision Controls - TODO: whole system could be better :) -- Flags
    public bool flying = false;
    public bool onGround = false;
    public bool onPlatform = false;
    public bool jumping = false;

    private float currentSpeed = 0.0f;

    private Bee bee;

    // Use this for initialization
    void Start() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }

        beeRigidbody = GetComponent<Rigidbody2D>();
        bee = GetComponent<Bee>();

        // Vector3 screenUpperCorner = new Vector3(Screen.width, Screen.height, 0f);
        // Vector3 worldUpperCorner = mainCamera.ScreenToWorldPoint(screenUpperCorner);
        // screenQuadWidth = worldUpperCorner.x;

        //private Vector3 worldTop;
        //Vector3 top = new Vector3(0f, Screen.height, 0f);
        //worldTop = mainCamera.ScreenToWorldPoint(top);
    }

    // Update is called once per frame
    void Update()
    {        
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
        // SmoothDamp the gravity on transition from Bee 
        beeRigidbody.gravityScale = 2.5f; 
        
        float jumpHeight = 12.0f;
        float movementSpeed = 0.075f;

        if (Input.GetKey(KeyCode.RightArrow))  // Move right
        {
            currentSpeed = movementSpeed;
            LookRight();
        } else if (Input.GetKey(KeyCode.LeftArrow))  // Move left
        {
            currentSpeed = -movementSpeed;
            LookLeft();
        } else
        {
            currentSpeed = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))  // Jump, now
        {
            if (onGround || onPlatform)
            {
                beeRigidbody.velocity = new Vector2(0f, jumpHeight);

                onGround = false;
                onPlatform = false;
                jumping = true;
            }
        }

        // Move
        Vector2 horizontalVector = new Vector2(currentSpeed, 0);
        beeRigidbody.position += horizontalVector;

        if (onGround || onPlatform)
        {
            jumping = false;
        }
    }

    private void UpdateObese()
    {
        beeRigidbody.gravityScale = 2.5f;
        float jumpHeight = 12.0f;
        float moveSpeed = 0.05f;

        if (Input.GetKey(KeyCode.RightArrow))  // Move right
        {
            currentSpeed = moveSpeed;
            LookRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))  // Move left
        {
            currentSpeed = -moveSpeed;
            LookLeft();
        } else
        {
            currentSpeed = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))  // Jump, now
        {
            if (onPlatform)  // Can only Jump on platform if obese
            {
                beeRigidbody.velocity = new Vector2(0f, jumpHeight);

                jumping = true;
                onGround = false;
                onPlatform = false;
            }
        }

        // Move
        Vector2 horizontalVector = new Vector2(currentSpeed, 0);
        beeRigidbody.position += horizontalVector;

        if (onGround || onPlatform)
        {
            jumping = false;
        } 
    }

    void FixedUpdate() {
        switch (bee.getCurrentBodyState())
        {
            case Bee.BodyState.Normal:
                FixedUpdateNormal();
                break;
            default:
                break;
        }
    }

    private void FixedUpdateNormal()
    {
        float upForce = 0.0f;
        float normalMovementSpeed = 3.0f;
        float horizontalMoveForce = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            if (!flying)  // initial burst when first hit space
            {
                beeRigidbody.velocity = new Vector2(0.0f, 0.25f);
            }

            upForce = flyForce * Time.fixedDeltaTime;            

            flying = true;
            onGround = false;
            onPlatform = false;
            beeRigidbody.gravityScale = 0.0f;  // no gravity while flying
        } else
        {
            flying = false;
            beeRigidbody.gravityScale = 0.25f;  // light gravity for small bee
        }

        // Debug.Log("UpForce = " + upForce.ToString());

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMoveForce = -normalMovementSpeed * Time.fixedDeltaTime;
            LookLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMoveForce = normalMovementSpeed * Time.fixedDeltaTime;
            LookRight();
        }

        // Fly
        Vector2 flyVector = new Vector2(0f, upForce);
        beeRigidbody.position += flyVector;

        // Move
        Vector2 horizontalVector = new Vector2(horizontalMoveForce, 0f);
        beeRigidbody.position += horizontalVector;
    }

    private void LookLeft()
    {
        transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);  // flip it left
        // heading = -1;
    }

    private void LookRight()
    {
        transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);  // flip it right
        // heading = 1;
    }
}
