using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ConfigIO
{
    #region Fields

    // configuration data with default values
    //static TowerField tower = new TowerField
    //{
    //    TargetingRange = 5f,
    //    RotationSpeed = 5f,
    //    Bps = 1f,
    //};

    TowerField defaultTowerField = new TowerField
    {
        TargetingRange = 3f,
        RotationSpeed = 200f,
        Bps = 1f,
        Cost = 10,
        UpdateCost_lv2 = 5,
        UpdateCost_lv3= 6,
    };

    MonsterField defaultMonsterField = new MonsterField { MaxHP = 100, MoveSpeed = 2f, Price= 3 };

    static ConfigIO instance;

    #endregion

    #region Properties

    //public float TargetingRange
    //{
    //    get { return targetingRange; }
    //}

    //public float RotationSpeed
    //{
    //    get { return rotationSpeed; }
    //}

    //public float Bps
    //{
    //    get { return bps; }
    //}
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    ConfigIO()
    {

    }



    public static ConfigIO GetInstance()
    {
        if (instance == null)
        {
            instance = new ConfigIO();
        }
        return instance;
    }


    #endregion

    public TowerField ReadTowerIO(string fileName, string filePath)
    {
        string ConfigurationDataFileName = fileName;
        string ConfigurationDataFilePath = Application.streamingAssetsPath + filePath;

        StreamReader input = null;
        try
        {
            input = File.OpenText(
                Path.Combine(
                   ConfigurationDataFilePath,
                    ConfigurationDataFileName
                ));

            string names = input.ReadLine();
            string values = input.ReadLine();

            return ConvertTowerConfigFields(values);
        }
        catch (Exception e)
        {

            //NotificationManager.AddNotification(new Notification { Title: "read tower " + fileName   ,  e.Message});
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $"read tower {fileName}",
                Message = e.Message
            });


            return null;
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    //Could not find a part of the path "C:\Users\ADMIN\Documents\PRU221m\Project\Main\TowerDefense\Assets\StreamingAssets\tower\turret\tower\turret\ConfigurationData.csvturret.csvturret.csv".
    public void CreateTowerIO(string fileName, string filePath, TowerField tower)
    {
        StreamWriter writer = null;
        string ConfigurationDataFileName = fileName;
        string ConfigurationDataFilePath = Application.streamingAssetsPath + filePath;
        try
        {
            if (Directory.Exists(ConfigurationDataFilePath))
            {
                string csvOutputFilePath = Path.Combine(
                  ConfigurationDataFilePath,
                   ConfigurationDataFileName
               );
                writer = new StreamWriter(
                   csvOutputFilePath);




                writer.WriteLine("TargetingRange,RotationSpeed,Bps, Cost, UpdateCost_lv2, UpdateCost_lv3");
                writer.WriteLine($"{tower.TargetingRange},{tower.RotationSpeed},{tower.Bps},{tower.Cost},{tower.UpdateCost_lv2}, {tower.UpdateCost_lv3}");
            }
            else
            {
                try
                {

                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(ConfigurationDataFilePath);
                }
                catch (Exception e)
                {

                    NotificationManager.GetInstance().AddNotification(new Notification
                    {
                        Title = $"create tower {fileName}",
                        Message = e.Message
                    });

                    CreateTowerIO(fileName, filePath, defaultTowerField);

                    //TextMeshProUGUI textMesh = (TextMeshProUGUI)GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
                    //Camera.main.GetComponent<Canvas>().GetComponent<TextMeshProUGUI>();
                    //textMesh.text = e.Message;
                }

            }

        }
        catch (Exception e)
        {
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $"write tower {fileName}",
                Message = e.Message
            });
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }

    TowerField ConvertTowerConfigFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        TowerField tower = new TowerField();
        try
        {

            //tower.TargetingRange = 5f;
            //tower.RotationSpeed = 5f;
            //tower.Bps = 1f;

            tower.TargetingRange = float.Parse(values[0]);
            tower.RotationSpeed = float.Parse(values[1]);
            tower.Bps = float.Parse(values[2]);
            tower.Cost = int.Parse(values[3]);
            tower.UpdateCost_lv2 = int.Parse(values[4]);
            tower.UpdateCost_lv3 = int.Parse(values[5]);


            //targetingRange = float.Parse(values[0]);
            //rotationSpeed = float.Parse(values[1]);
            //bps = float.Parse(values[2]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $" tower : value {csvValues}",
                Message = e.Message
            });
            tower = defaultTowerField;
        }
        return tower;


    }


    public MonsterField ReadMonsterIO(string fileName, string filePath)
    {
        string ConfigurationDataFileName = fileName;
        string ConfigurationDataFilePath = Application.streamingAssetsPath + filePath;

        StreamReader input = null;
        try
        {
            input = File.OpenText(
                Path.Combine(
                   ConfigurationDataFilePath,
                    ConfigurationDataFileName
                ));

            string names = input.ReadLine();
            string values = input.ReadLine();

            return ConvertMonsterConfigFields(values);
        }
        catch (Exception e)
        {
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $" Read Monster {fileName}",
                Message = e.Message
            });
            return null;
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    public void CreateMonsterIO(string fileName, string filePath, MonsterField monster)
    {
        StreamWriter writer = null;
        string ConfigurationDataFileName = fileName;
        string ConfigurationDataFilePath = Application.streamingAssetsPath + filePath;
        try
        {
            if (Directory.Exists(ConfigurationDataFilePath))
            {
                string csvOutputFilePath = Path.Combine(
                  ConfigurationDataFilePath,
                   ConfigurationDataFileName
               );
                writer = new StreamWriter(
                   csvOutputFilePath);




                writer.WriteLine("MaxHP,MoveSpeed,Price");
                writer.WriteLine($"{monster.MaxHP},{monster.MoveSpeed},{monster.Price}");
            }
            else
            {
                try
                {

                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(ConfigurationDataFilePath);
                }
                catch (Exception e)
                {
                    NotificationManager.GetInstance() .AddNotification(new Notification
                    {
                        Title = $" create Monster {fileName}",
                        Message = e.Message
                    });

                    CreateTowerIO(fileName, filePath, defaultTowerField);

                    //TextMeshProUGUI textMesh = (TextMeshProUGUI)GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
                    //Camera.main.GetComponent<Canvas>().GetComponent<TextMeshProUGUI>();
                    //textMesh.text = e.Message;
                }

            }

        }
        catch (Exception e)
        {
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $" Write Monster {fileName}",
                Message = e.Message
            });
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }

    MonsterField ConvertMonsterConfigFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        MonsterField monster = new MonsterField();
        try
        {


            monster.MaxHP = int.Parse(values[0]);
            monster.MoveSpeed = float.Parse(values[1]);
            monster.Price = int.Parse(values[2]);



        }
        catch (Exception e)
        {
            NotificationManager.GetInstance().AddNotification(new Notification
            {
                Title = $"Monster: {csvValues}",
                Message = e.Message
            });
            Console.WriteLine(e);

            monster = defaultMonsterField;
        }
        return monster;


    }


}
