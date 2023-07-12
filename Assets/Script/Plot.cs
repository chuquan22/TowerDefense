using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public static Plot main;
    public GameObject tower;
    public Turret turret;
    private Color startColor;

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
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI())
        {
            return;
        }
        if (tower != null)
        {
            turret.OpenUpgradeUI();
            return;
        }
        try { 
            TowerTest towerToBuild = BuildManager.main.GetSelectedTower();
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            BuildManager.main.SetDefaultSelectedTower();
            turret = tower.GetComponent<Turret>();
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        

        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
    }

}
