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
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(Continue);
        btnRestart.onClick.AddListener(Restart);
        btnQuit.onClick.AddListener(QuitGame);
    }

    void Continue()
    {

    }

     void Restart()
     {
        SceneManager.LoadScene("SampleGame");
    }

    void QuitGame()
    {
        SceneManager.LoadScene("StartGame");
    }
}
