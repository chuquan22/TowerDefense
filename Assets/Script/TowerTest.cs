using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TowerTest 
{
    public string name;
    public int cost;
    public GameObject prefab;

    

    public TowerTest(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
