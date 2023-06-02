using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public Button btnContinue;
    public Button btnQuit;
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(Continue);
        btnQuit.onClick.AddListener(QuitGame);
    }

    void Continue()
    {

    }

    void QuitGame()
    {
        // end program
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
