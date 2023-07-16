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
    public Tower turret;
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
        if (BuildManager.main.GetSelectedTower() != null)
        {
            TowerTest towerToBuild = BuildManager.main.GetSelectedTower();
            // if money player bigger or equal tower's price
            if (MonsterSpawner.price >= towerToBuild.cost)
            {
                MonsterSpawner.price = MonsterSpawner.price - towerToBuild.cost;
                MonsterSpawner.isTowerBought = true;
                tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                turret = tower.GetComponent<Tower>();
                BuildManager.main.SetDefaultSelectedTower();
            }
            else
            {
                NotificationManager.AddNotification(new Notification
                {
                    Title = "Warning",
                    Message = "Not enough price to buy"
                });
            }
        }
        
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
    }

}
