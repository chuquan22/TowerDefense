using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInit : MonoBehaviour
{

    void Awake()
    {
        // initialize configuration utils
        ConfigUtils.InitializeData();
    }
  
}
