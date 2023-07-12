using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private TowerTest[] towers;
    private int selectedTower = -1;


    private void Awake()
    {
        main = this;
    }
    public TowerTest GetSelectedTower()
    {
        return towers[selectedTower];
    }
    public void SetSeclectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower; 
    }
    public void SetDefaultSelectedTower()
    {
        selectedTower = -1;
    }
}
