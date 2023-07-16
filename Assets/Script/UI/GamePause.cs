using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public Button btnContinue;
    public Button btnRestart;
    public Button btnQuit;
    public GameObject image;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(Continue);
        btnRestart.onClick.AddListener(Restart);
        btnQuit.onClick.AddListener(QuitGame);
        image.SetActive(false);
    }

    public void Continue()
    {
        //image.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Quan");

        GameObject.Find("Level").GetComponent<MonsterSpawner>().ResetGame();
        Time.timeScale = 1f;

    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartGame");
    }

    public void SetPause()
    {

        image.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (isPaused) {
            image.SetActive(true);
            
        }
        else
        {
            image.SetActive(false);
        }
    }
}
