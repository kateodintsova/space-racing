using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControllerScript : MonoBehaviour
{
    public Text scoreText;
    public Text shieldsText;
    public TextMeshProUGUI pauseText;
    public Button restartButton;
    public Button menuButton;
    public bool gameIsActive;

    int score = 0;

    public void incrementScore (int addition)
    {
        score += addition;
        scoreText.text = "Score: " + score;
    }

    public void countShields(int shields)
    {
        shieldsText.text = "Shields: " + shields;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = true;
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pauseText.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseText.gameObject.SetActive(false);
        }            
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameIsActive = false;
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
