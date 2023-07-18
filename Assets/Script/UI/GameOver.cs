using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button btnRestart;
    public Button btnMenu;
    public Button btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        //btnRestart.onClick.AddListener(Restart);
        //btnMenu.onClick.AddListener(Menu);
        //btnQuit.onClick.AddListener(Quit);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Quan");
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
