using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public GameObject startPoint;
    public GameObject[] path;
    public string direction;
    private void Awake()
    {
        main = this;
    }
}
