using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{

    public Text CoinText;
    public Text LivesText;
    public Text GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        CoinText.text = "Coins: " + GameManager.ManagerInstance.coins;
        LivesText.text = "Lives: " + GameManager.ManagerInstance.lives;
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = "Coins: " + GameManager.ManagerInstance.coins;
        LivesText.text = "Lives: " + GameManager.ManagerInstance.lives;
        if(!GameOverText.enabled && GameManager.ManagerInstance.lives == 0) {
            GameOverText.enabled = true;
        }
    }
}
