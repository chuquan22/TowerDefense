using System;
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
    static MonsterField BeeMonster;
    static MonsterField BoneMonster;
    static MonsterField ScorpionMonster;
    static MonsterField FireBugMonster;
    static MonsterField BugMonster;
    static MonsterField ButterflyMonster;
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

    public static MonsterField GetBeeMonsterField()
    {
        return BeeMonster;
    }
    public static MonsterField GetFireBugMonsterField()
    {
        return FireBugMonster;
    }
    public static MonsterField GetBugMonsterField()
    {
        return BugMonster;
    }
    public static MonsterField GetBoneMonsterField()
    {
        return BoneMonster;
    }
    public static MonsterField GetScorpionMonsterField()
    {
        return ScorpionMonster;
    }
    public static MonsterField GetButterflyMonsterField()
    {
        return ButterflyMonster;
    }
    #endregion

    #region Initial

    static ConfigIO io = ConfigIO.GetInstance();
    public static void InitializeData()
    {

        try
        {
    InitIceTower();
        InitRockTower();
        InitShockTower();
        InitLightTower();

        InitBeeMonster();
        InitBoneMonster();
        InitScorpionMonsterr();
        InitFireBugMonster();
        InitBugMonster();
        InitButterflyMonster();


        }
        catch (Exception e)
        {

            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $"Init file Error",
                Message = e.Message
            });

        }

    }


    static void InitIceTower()
    {
        string fileName = "IceTower.csv";
        string filePath = "/Tower";

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
        string filePath = "/Tower";

        RockTower = io.ReadTowerIO(fileName: fileName, filePath: filePath);

        if (RockTower == null)
        {
            TowerField value = new TowerField
            {

                TargetingRange = 4,
                RotationSpeed = 5,
                Bps = 1,

                Cost = 20,
                UpdateCost_lv2 = 40,
                UpdateCost_lv3 = 80

            };
            io.CreateTowerIO(fileName: fileName, filePath: filePath, value);
            InitRockTower();
        }
    }
    static void InitShockTower()
    {
        string fileName = "ShockTower.csv";
        string filePath = "/Tower";

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
        string filePath = "/Tower";

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
    static void InitBeeMonster()
    {
        string fileName = "BeeMonster.csv";
        string filePath = "/Monster";

        BeeMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (BeeMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 40,
                MoveSpeed = 2f,
                Price = 10
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitBeeMonster();
        }
    }

    static void InitBoneMonster()
    {
        string fileName = "BoneMonster.csv";
        string filePath = "/Monster";

        BoneMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (BoneMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 250,
                MoveSpeed = 3f,
                Price = 15
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitBoneMonster();
        }
    }


    static void InitScorpionMonsterr()
    {
        string fileName = "ScorpionMonster.csv";
        string filePath = "/Monster";

        ScorpionMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (ScorpionMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 200,
                MoveSpeed = 2.5f,
                Price = 8
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitScorpionMonsterr();
        }
    }

    static void InitFireBugMonster()
    {
        string fileName = "FireBugMonster.csv";
        string filePath = "/Monster";

        FireBugMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (FireBugMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 100,
                MoveSpeed = 2.3f,
                Price = 8
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitFireBugMonster();
        }
    }


    static void InitBugMonster()
    {
        string fileName = "BugMonster.csv";
        string filePath = "/Monster";

        BugMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (BugMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 70,
                MoveSpeed = 2.5f,
                Price = 10
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitBugMonster();
        }
    }
    static void InitButterflyMonster()
    {
        string fileName = "ButterflyMonster.csv";
        string filePath = "/Monster";

        ButterflyMonster = io.ReadMonsterIO(fileName: fileName, filePath: filePath);

        if (ButterflyMonster == null)
        {
            MonsterField value = new MonsterField
            {

                MaxHP = 80,
                MoveSpeed = 3.5f,
                Price = 8
            };
            io.CreateMonsterIO(fileName: fileName, filePath: filePath, monster: value);
            InitButterflyMonster();
        }
    }
    #endregion 




}
