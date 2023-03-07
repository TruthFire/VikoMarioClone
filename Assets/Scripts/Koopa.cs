using System.Collections;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float ShellSpeed = 12f;
    
    private bool isInShell;
    private bool isShellMoving;

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(!isInShell && collision.gameObject.CompareTag("Player")) {

            Player player = collision.gameObject.GetComponent<Player>();

            if(player.starpower) {
                Hit();
            } else if( collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isInShell && other.CompareTag("Player")) {
            if(!isShellMoving) {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);

            }
            else {
                 Player player = other.GetComponent<Player>();
                 if(player.starpower) {
                    Hit();
                 } else {
                    player.Hit();
                 }
            }
        } 
         if(!isInShell && other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }

    private void PushShell(Vector2 direction) {
        isShellMoving = true;

        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();

        movement.direction = direction.normalized;
        movement.speed = ShellSpeed;
        movement.enabled = true;     

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void EnterShell() 
    {
        isInShell = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteAnimationManager>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void Hit() {
        GetComponent<SpriteAnimationManager>().enabled = false;
        GetComponent<EntityDeathAnimation>().enabled = true;

        Destroy(gameObject,3f);
    }

    private void OnBecameInvisible() {
        if(isShellMoving) {
            Destroy(gameObject);
        }
    }
}
