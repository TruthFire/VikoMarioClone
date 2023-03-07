using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ManagerInstance {get; private set; }

    public int world {get; private set;}
    public int stage {get; set;}
    public int lives {get; set;}

    private void Awake()
    {
        if (ManagerInstance != null) {
            DestroyImmediate(gameObject);
        } else {
            ManagerInstance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void OnDestroy() {
        if (ManagerInstance == this) {
            ManagerInstance = null;
        }   
    }

    private void Start() {
        NewGame(); 
    }   

    private void NewGame() {
        lives = 3;
        LoadLevel(1,1);
    }

    private void LoadLevel(int world, int stage) {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel() {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay) {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel() {
        lives--;
        
        if(lives > 0) {
            LoadLevel(world, stage);
        }

        else {
            GameOver();
        }
    }

    private void GameOver() {
        Invoke(nameof(NewGame), 3f);
    }

}