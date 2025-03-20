using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Game,
        GameOver,
        Win
    }
    public static GameManager instance;

    [HideInInspector]
    public State gameState;
    public int score = 0;
    void Start()
    {
        instance = this;
        gameState = State.Menu;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetScore(int value)
    {
        score += value;
        UIController.instance.UpdateScore();
        CheckGameOver();
    }
    public void StartGame()
    {
        gameState = State.Game;
        score = 0;
        foreach (var ballonData in SpawnManager.instance.ballonData)
        {
            ballonData.hit = 0;
        }
        UIController.instance.UpdateScore();
    }
    public void GoToMenu()
    {
        gameState = State.Menu;
    }
    private void CheckGameOver()
    {
        if (score >= 50)
        {
            gameState = State.Win;
            UIController.instance.GameOver();
            BallonController.DestroyAllBallons();
        }
        else if (score <= 0)
        {
            gameState = State.GameOver;
            UIController.instance.GameOver();
            BallonController.DestroyAllBallons();
        }
    }
}
