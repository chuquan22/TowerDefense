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
    

    public void Restart()
    {
        SceneManager.LoadScene("Quan");
        MonsterSpawner monster = new MonsterSpawner();
        monster.ResetGame();
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
