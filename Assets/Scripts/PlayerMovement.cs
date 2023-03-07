using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    private Rigidbody2D rbody;
    private float inputAxis;
    private Vector2 velocity;

    public float moveSpeed = 8f;


    private void Awake() {
        rbody = GetComponent<Rigidbody2D>(); 
    }

    public void Update() {
        HorizonalMovement();
    }

    private void HorizonalMovement() {
        inputAxis = Input.GetAxis("Horizontal");

        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime) ;
    }

    private void FixedUpdate() {
        Vector2 position = rbody.position;
        position += velocity * Time.fixedDeltaTime;

        rbody.MovePosition(position);
    }
    
}
