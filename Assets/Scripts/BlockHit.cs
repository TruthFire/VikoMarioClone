using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{

    public GameObject item;


    public int maxHits = -1;
    public Sprite emptyBlock;

    private bool isAnimating;
    

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!isAnimating && maxHits != 0 && collision.gameObject.CompareTag("Player")) {
            if(collision.transform.DotTest(transform, Vector2.up)) {
                Hit();
            }
        } 
    }

    private void Hit() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHits--;
        if(maxHits == 0) {
            spriteRenderer.sprite = emptyBlock;
        }
        
        if(item != null) {
            Instantiate(item, transform.position, Quaternion.identity);

        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate() {

        isAnimating = true;

        Vector3 defaultPos = transform.localPosition;
        Vector3 animatedPos = defaultPos + Vector3.up * 0.5f;

        yield return Move(defaultPos, animatedPos);
        yield return Move(animatedPos, defaultPos);

        isAnimating = false;

    }

    private IEnumerator Move(Vector3 from, Vector3 to) {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
