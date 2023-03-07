using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaManager : MonoBehaviour
{
   public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            if( collision.transform.DotTest(transform, Vector2.down)) {
                Flaten();
            }
        }
    }

    private void Flaten() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteAnimationManager>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

}
