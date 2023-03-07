using System.Collections;
using UnityEngine;

public class EntityDeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    private void Reset() {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable() {
        UpdateSprite();
        DisablePhysics();

        StartCoroutine(Animate());
        
    }

    private void UpdateSprite() {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if(deathSprite != null) {
            spriteRenderer.sprite = deathSprite;
        }
    }

    private void DisablePhysics() {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerMovement pm = GetComponent<PlayerMovement>();
        EntityMovement em = GetComponent<EntityMovement>();

        if(pm != null) {
            pm.enabled = false;
        }
        if(em != null) {
            em.enabled = false;
        }
    }

    private IEnumerator Animate() {

        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while(elapsed < duration) {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }


    }
}
