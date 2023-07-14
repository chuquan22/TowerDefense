using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigUtils
{
    #region Tower Value
    static TowerField IceTower;
    static TowerField RockTower;
    static TowerField ShockTower;
    static TowerField LightTower;
    #endregion

    #region Monster Value
    static MonsterField FlyMonster;
    static MonsterField WalkMonster;
    #endregion


    #region
    public static TowerField GetTowerIceField()
    {
        return IceTower;
    }
    public static TowerField GetTowerRockeField()
    {
        return RockTower;
    }
    public static TowerField GetTowerShockField()
    {
        return ShockTower;
    }
    public static TowerField GetTowerLightField()
    {
        return LightTower;
    }

    public static MonsterField GetMonsterFlyField()
    {
        return FlyMonster;
    }
    #endregion

    #region Initial

    static ConfigIO io = ConfigIO.GetInstance();
    public static void InitializeData()
    {
        InitIceTower();
        InitRockTower();
        InitShockTower();
        InitLightTower();

        InitFlyMonster();
    }


    static void InitIceTower()
    {
        string fileName = "IceTower.csv";
        string filePath = "Tower";

        IceTower = io.ReadTowerIO(fileName: fileName, filePath: filePath);

        if (IceTower == null)
        {
            TowerField value = new TowerField
            {

                TargetingRange = 3,
                RotationSpeed = 200,
                Bps = 1,
                Cost = 20,
                UpdateCost_lv2 = 40,
                UpdateCost_lv3 = 80
            };

            io.CreateTowerIO(fileName: fileName, filePath: filePath, value);
            InitIceTower();
        }
    }

    static void InitRockTower()
    {
        string fileName = "RockTower.csv";
        string filePath = "Tower";

        RockTower = io.ReadTowerIO(fileName: fileName, filePath: filePath);

        if (RockTower == null)
        {
            TowerField value = new TowerField
            {

                TargetingRange = 4,
                RotationSpeed = 5,
                Bps = 1
            };
            io.CreateTowerIO(fileName: fileName, filePath: filePath, value);
            InitRockTower();
        }
    }
    static void InitShockTower()
    {
        string fileName = "ShockTower.csv";
        string filePath = "Tower";

        ShockTower = io.ReadTowerIO(fileName: fileName, filePath: filePath);

        if (ShockTower == null)
        {
            TowerField value = new TowerField
            {

                TargetingRange = 3,
                RotationSpeed = 0,
                Bps = 1,
                Cost = 30,
                UpdateCost_lv2 = 50,
                UpdateCost_lv3 = 100
            };
            io.CreateTowerIO(fileName: fileName, filePath: filePath, value);
            InitShockTower();
        }
    }
    static void InitLightTower()
    {
        string fileName = "LightTower.csv";
        string filePath = "Tower";

        LightTower = io.ReadTowerIO(fileName: fileName, filePath: filePath);

        if (LightTower == null)
        {
            TowerField value = new TowerField
            {

                TargetingRange = 4,
                RotationSpeed = 0,
                Bps = 2,
                Cost = 35,
                UpdateCost_lv2 = 60,
                UpdateCost_lv3 = 100
            };
            io.CreateTowerIO(fileName: fileName, filePath: filePath, value);
            InitLightTower();
        }
    }
    static void InitFlyMonster()
    {
        string fileName = "FlyMonster.csv";
        string filePath = "Monster";

        FlyMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (FlyMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 100,
                MoveSpeed = 2f,
                Price = 10
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitFlyMonster();
        }
    }



    #endregion 




}
