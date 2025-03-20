using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    GameObject gameUI, menuUI, gameOverUI;

    [SerializeField]
    TextMeshProUGUI scoreText, gameOverText;
    [SerializeField]
    Transform content;
    [SerializeField]
    GameObject hitView;

    void Start()
    {
        instance = this;
        SwitchCanvas(menuUI);
    }

    // Update is called once per frame
    public void StartGame()
    {
        SwitchCanvas(gameUI);
        GameManager.instance.StartGame();
        SoundManager.instance.PlayButton();
    }
    public void QuitGame()
    {
        Application.Quit();
        SoundManager.instance.PlayButton();
    }
    public void GoToMenu()
    {
        SwitchCanvas(menuUI);
        GameManager.instance.GoToMenu();
        SoundManager.instance.PlayButton();
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }
    public void GameOver()
    {
        SwitchCanvas(gameOverUI);
        if (GameManager.instance.gameState == GameManager.State.Win)
        {
            gameOverText.text = "You Win!";
        }
        else
        {
            gameOverText.text = "Game Over!";
        }

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (BallonData ballonData in SpawnManager.instance.ballonData)
        {
            Instantiate(hitView, content).GetComponent<HitView>().SetView(ballonData);
        }

        StartCoroutine(UpdateLayout());

    }
    private IEnumerator UpdateLayout()
    {
        yield return null; // Bir frame bekle
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());


    }

    private void SwitchCanvas(GameObject canvas)
    {
        gameUI.SetActive(false);
        menuUI.SetActive(false);
        gameOverUI.SetActive(false);

        canvas.SetActive(true);
    }
}
