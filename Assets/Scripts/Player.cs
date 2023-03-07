using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteManager smallRenderer;
    public PlayerSpriteManager bigRenderer;

    private EntityDeathAnimation deathAnimation;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake() {
        deathAnimation = GetComponent<EntityDeathAnimation>();
    }

    public void Hit() {

        if(big) {

            //shrink
        } else {
            PlayerDie();
        }

    }

    private void PlayerDie() {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.ManagerInstance.ResetLevel(3f);
    }
}
