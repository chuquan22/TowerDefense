using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Button buttonStart;
    public Button buttonQuit;

    void Start()
    {
        //buttonStart.onClick.AddListener(StartGame);
        //buttonQuit.onClick.AddListener(QuitGame);
    }

   public  void StartGame()
    {
        SceneManager.LoadScene("Quan");
    }

    public void QuitGame()
    {
        // end program
      //  UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


    void Update()
    {
        
    }
}
