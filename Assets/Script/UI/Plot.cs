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
    public static bool isNotEnoughMoney = false;
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
        TowerTest towerToBuild = BuildManager.main.GetSelectedTower();
        Debug.Log("Current price : " + MonsterSpawner.price);
        Debug.Log("Bigger: " + (MonsterSpawner.price >= towerToBuild.cost)); 

        if (tower != null)
        {
            turret.OpenUpgradeUI();
            return;
        }
        try {
            // if money player bigger or equal tower's price
            if (MonsterSpawner.price >= towerToBuild.cost)
            {
                MonsterSpawner.price = MonsterSpawner.price - towerToBuild.cost;
                // Debug.Log("Tower price : " + towerToBuild.cost);
                MonsterSpawner.isTowerBought = true;
                tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                turret = tower.GetComponent<Turret>();
            }
            else
            {
                isNotEnoughMoney = true;
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        gameObject.SetActive(false);

        /*
        if (UIManager.main.IsHoveringUI())
        {
            return;
        }
        */
    }

    public void Upgrade()
    {
    }

}
