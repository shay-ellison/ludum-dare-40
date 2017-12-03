using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    public Camera mainCamera;
    public Rigidbody2D beeRigidbody;

    private float flyForce = 1.0f;  // needs to beat gravity, TODO: calculate based on gravity?

    // Position/Collision Controls - TODO: whole system could be better :) -- Flags
    public bool flying = false;
    public bool onGround = false;
    public bool onPlatform = false;
    public bool jumping = false;
    // public bool treeCollision = false;

    private Bee bee;

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

        //private Vector3 worldTop;
        //Vector3 top = new Vector3(0f, Screen.height, 0f);
        //worldTop = mainCamera.ScreenToWorldPoint(top);
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
        beeRigidbody.gravityScale = 0.25f;
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
        Vector2 horizontalVector = new Vector2(horizontalX, 0);
        beeRigidbody.position += horizontalVector;

        if (onGround || onPlatform)
        {
            jumping = false;
        }
    }

    private void UpdateObese()
    {
        beeRigidbody.gravityScale = 0.25f;
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
                beeRigidbody.velocity = new Vector2(0, 0.8f);

                onGround = false;
                onPlatform = false;
                jumping = true;
            }
        }

        // Move
        //if (!treeCollision && (jumping || onGround || onPlatform))
        //{
        Vector2 horizontalVector = new Vector2(horizontalX, 0);
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
        float horizontalMoveForce = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            if (!flying)
            {
                beeRigidbody.velocity = new Vector2(0.0f, 0.5f);
            } else
            {
                upForce += flyForce * Time.fixedDeltaTime;
            }            

            flying = true;
            onGround = false;  // might be on ground until you apply an upForce of some sort
            beeRigidbody.gravityScale = 0.0f;
        } else
        {
            flying = false;
            beeRigidbody.gravityScale = 0.25f;
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

        // Fly
        Vector2 flyVector = new Vector2(0f, upForce);
        beeRigidbody.position += flyVector;

        // Move
        if (flying)
        {
            Vector2 horizontalVector = new Vector2(horizontalMoveForce, 0f);
            beeRigidbody.position += horizontalVector;
        }
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
