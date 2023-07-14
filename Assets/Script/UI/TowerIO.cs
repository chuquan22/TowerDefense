using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TowerIO
{
    #region Fields

    static string ConfigurationDataFileName = "";
    static string ConfigurationDataFilePath;

    // configuration data with default values
    //static TowerField tower = new TowerField
    //{
    //    TargetingRange = 5f,
    //    RotationSpeed = 5f,
    //    Bps = 1f,
    //};

    static float targetingRange = 5f;
    static float rotationSpeed = 5f;
    static float bps = 1f;

    #endregion

    #region Properties

    public float TargetingRange
    {
        get { return targetingRange; }
    }

    public float RotationSpeed
    {
        get { return rotationSpeed; }
    }

    public float Bps
    {
        get { return bps; }
    }
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public TowerIO(string fileName, string filePath)
    {
        ConfigurationDataFileName = fileName;
        ConfigurationDataFilePath = Application.streamingAssetsPath + filePath;




        // read and save configuration data from file
        readFileConfig();
    }


    static void readFileConfig()
    {
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

            SetConfigFields(values);
        }
        catch (Exception e)
        {

            createFileConfig();
            readFileConfig();
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
    static void createFileConfig()
    {
        StreamWriter writer = null;
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




                writer.WriteLine("TargetingRange,RotationSpeed,Bps");
                writer.WriteLine("5f,5f,1f");
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

                    createFileConfig();
                    //TextMeshProUGUI textMesh = (TextMeshProUGUI)GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
                    //Camera.main.GetComponent<Canvas>().GetComponent<TextMeshProUGUI>();
                    //textMesh.text = e.Message;
                }

            }

        }
        catch (Exception e)
        {

        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }


    #endregion

    static void SetConfigFields(string csvValues)
    {
        string[] values = csvValues.Split(',');

        try
        {
            //tower.TargetingRange = 5f;
            //tower.RotationSpeed = 5f;
            //tower.Bps = 1f;

            //tower.TargetingRange = float.Parse(values[0]);
            //tower.RotationSpeed = float.Parse(values[1]);
            //tower.Bps = float.Parse(values[2]);


            targetingRange = float.Parse(values[0]);
            rotationSpeed = float.Parse(values[1]);
            bps = float.Parse(values[2]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            targetingRange = 5f;
            rotationSpeed = 5f;
            bps = 1f;
        }
    }

}
