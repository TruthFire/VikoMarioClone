using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    private Camera cam;
    private Rigidbody2D rbody;
    private float inputAxis;
    private Vector2 velocity;

    public float moveSpeed = 8f;
    public float MaxJumpHeight = 5f;
    public float MaxJumpTime = 1f;
    public float jumpForce => (2f * MaxJumpHeight) / (MaxJumpTime / 2f);
    public float gravity => (-2f * MaxJumpHeight) / Mathf.Pow((MaxJumpTime / 2f),2);

    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }
    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool isSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);



    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        cam = Camera.main; 
    }

    public void Update() {

        HorizonalMovement();
        
        isGrounded = rbody.Raycast(Vector2.down);      

        if(isGrounded) {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void ApplyGravity() {
        bool isFalling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2f : 1f;

        velocity.y += gravity * Time.deltaTime * multiplier;

        velocity.y = MathF.Max(velocity.y, gravity / 2f);
    }

    private void GroundedMovement() {

        velocity.y = MathF.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f ;

        if(Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce;
            isJumping = true;
        }
    }

    private void HorizonalMovement() {
        inputAxis = Input.GetAxis("Horizontal");

        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rbody.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        if(velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate() {
        Vector2 position = rbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer ==  LayerMask.NameToLayer("Enemy")) {
            if(transform.DotTest(collision.transform, Vector2.down)) {
                velocity.y = jumpForce / 2;
                isJumping = true;
            }
        } else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) {

            if(transform.DotTest(collision.transform, Vector2.up)) {
                velocity.y = 0;
            }
        }
    }
    
}
