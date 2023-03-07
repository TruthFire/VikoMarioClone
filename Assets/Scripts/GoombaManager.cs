using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaManager : MonoBehaviour
{
   public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.CompareTag("Player")) {

            Player player = collision.gameObject.GetComponent<Player>();

            if( collision.transform.DotTest(transform, Vector2.down)) {
                Flaten();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }


    private void Hit() {
        GetComponent<SpriteAnimationManager>().enabled = false;
        GetComponent<EntityDeathAnimation>().enabled = true;

        Destroy(gameObject,3f);
    }

    private void Flaten() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteAnimationManager>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

}
