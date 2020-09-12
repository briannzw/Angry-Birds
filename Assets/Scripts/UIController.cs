using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject nextLevelButton;
    public GameObject pausePanel;
    public GameObject text;


    private void Start()
    {
        nextLevelButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }

    public void NextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame(string msg, bool nextLevel)
    {
        text.GetComponent<Text>().text = msg;
        nextLevelButton.SetActive(nextLevel);
        pausePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
