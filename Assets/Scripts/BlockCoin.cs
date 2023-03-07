using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ManagerInstance.AddCoin();

        StartCoroutine(Animate());        
    }

    private IEnumerator Animate() {


        Vector3 defaultPos = transform.localPosition;
        Vector3 animatedPos = defaultPos + Vector3.up * 1.5f;

        yield return Move(defaultPos, animatedPos);
        yield return Move(animatedPos, defaultPos);

        Destroy(gameObject);

    }

    private IEnumerator Move(Vector3 from, Vector3 to) {
        float elapsed = 0f;
        float duration = 0.25f;

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
