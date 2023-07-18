using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator ani;

    private bool isMenuOpen = true;


    private void Update()
    {
       
    }
    public void CloseMenu()
    {

        GameObject.Find("Level").GetComponent<BuildManager>().currentPlot.GetComponent<Plot>().blurPlot();
        GameObject.Find("Level").GetComponent<BuildManager>().currentPlot = null;
        ani.SetBool("MenuOpen", false);
    }

   


}
