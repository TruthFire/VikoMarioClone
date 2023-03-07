using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
   private void Start() {
    StartCoroutine(Animate());
   }

   private IEnumerator Animate() {

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        Collider2D physicscollider = GetComponent<Collider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rbody.isKinematic = true;
        physicscollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + Vector3.up;

        while (elapsed < duration) {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPos;

        rbody.isKinematic = false;
        physicscollider.enabled = true;
        triggerCollider.enabled = true;
   }


}
