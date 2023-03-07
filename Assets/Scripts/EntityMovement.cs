using UnityEngine;

public class EntityMovement : MonoBehaviour
{

    public float speed = 1f;
    public Vector2 direction = Vector2.left;


    private Rigidbody2D rbody;
    private Vector2 velocity;

    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        enabled = false;


    }

    private void OnBecameVisible() {
        enabled = true;
    }

    private void OnBecameInvisible(){
        enabled = false;

    }

    private void OnEnable() {
        rbody.WakeUp();
    }

    private void OnDisable() {
        rbody.velocity = Vector2.zero;
        rbody.Sleep();
    }

    private void FixedUpdate() {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rbody.MovePosition(rbody.position + velocity * Time.fixedDeltaTime);

        if (rbody.Raycast(direction)) {
            direction = -direction;
        }

        if (rbody.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    
}
