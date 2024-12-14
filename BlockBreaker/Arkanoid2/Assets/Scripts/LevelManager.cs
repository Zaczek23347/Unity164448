using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;

    public int currentSceneIndex;

    public int brickCount;
    public GameObject youWinPanel;
    private AudioSource audioSource; // Zmienna AudioSource

    // Start is called before the first frame update
    void Start()
    {
        brickCount = GameObject.FindGameObjectsWithTag("brick").Length +
                     GameObject.FindGameObjectsWithTag("multiply").Length +
                     GameObject.FindGameObjectsWithTag("life").Length;
        Time.timeScale = 1;
        if (audioSource != null)
        {
            audioSource.Play(); // Odtwarzanie dŸwiêku
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if(brickCount <= 0)
        {
            youWinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        if (currentSceneIndex == 3)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
    }
}
