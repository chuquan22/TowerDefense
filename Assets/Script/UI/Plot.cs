using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform hoverColor;

    public static Plot main;
    public GameObject tower;
   // public Tower turret;
    private Color startColor;

    private static Vector3 normal = new Vector3(1, 1, 1);
    private static Vector3 focused = normal * (2);

    private void Update()
    {
        if (tower != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
    }
    public void focusPlot()
    {
        gameObject.transform.localScale = focused;
    }

    public void blurPlot()
    {
        gameObject.transform.localScale = normal;
    }
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        startColor = sr.color;

    }
    private void OnMouseEnter()
    {

        if (GameObject.Find("Level").GetComponent<BuildManager>().getPlotSelected() == null && tower==null)
        {
            focusPlot();
        }
     
    }
    private void OnMouseExit()
    {
        if (GameObject.Find("Level").GetComponent<BuildManager>(). getPlotSelected()==null)
        {
            blurPlot();
        }
      
    }
    private void OnMouseDown()
    {
        if (GameObject.Find("Level").GetComponent<BuildManager>().getPlotSelected() == null)
        {
            if (UIManager.main.IsHoveringUI())
            {
                return;
            }
            if (tower != null)
            {
                tower.GetComponent<Tower>().OpenUpgradeUI();
                return;
            }
            else
            {
                ShowUgradeMenu();
                focusPlot();
                GameObject.Find("Level").GetComponent<BuildManager>().setPlotSelected(gameObject);
            }
        }

    }


    void ShowUgradeMenu()
    {
        GameObject.Find("Menu").GetComponent<Animator>().SetBool("MenuOpen", true);

    }
    public void Upgrade()
    {

    }

}
