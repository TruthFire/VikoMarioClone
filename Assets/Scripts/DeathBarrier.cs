using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player")) {
        other.gameObject.SetActive(false);
        GameManager.ManagerInstance.ResetLevel(3f);
    }

    else {
        Destroy(other.gameObject);
    }
   }
}
