using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private TowerTest[] towers;
    private int selectedTower =-1;
    private GameObject currentPlot;


    private void Awake()
    {
        main = this;
        GameObject.Find("Menu").GetComponent<Animator>().SetBool("MenuOpen", false);
    }

  
    public TowerTest GetSelectedTower()
    {
        return selectedTower<0 ? null: towers[selectedTower];
    }

    public void setPlotSelected( GameObject p)
    {
        currentPlot = p;
    }

    public GameObject getPlotSelected()
    {
        return currentPlot;
    }

    private void ResetPlot()
    {
        currentPlot.GetComponent<Plot>().blurPlot();

        currentPlot = null;
    }
    public void SetSeclectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;

        GameObject.Find("Menu").GetComponent<Animator>().SetBool("MenuOpen", false);

        if (MonsterSpawner.price >= GetSelectedTower().cost)
        {
            MonsterSpawner.price = MonsterSpawner.price - GetSelectedTower().cost;
            MonsterSpawner.isTowerBought = true;
            currentPlot.GetComponent<Plot>(). tower = Instantiate(GetSelectedTower().prefab,  currentPlot.transform.position,  Quaternion.identity);
           SetDefaultSelectedTower();
           
            //  gameObject.SetActive(false);
        }
        else
        {
            NotificationManager.AddNotification(new Notification
            {
                Title = "Warning",
                Message = "Not enough price to buy"
            });
        }

        ResetPlot();


        //   selectedTower = _selectedTower; 
    }
    public void SetDefaultSelectedTower()
    {
        selectedTower = -1;
    }
}
