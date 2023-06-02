using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MagicTower : Tower
{


   //  string g = GetType().Name;
        // typeof(MagicTower).FullName; 
    const string ConfigurationDataFileName = "MagicTowerConfiguration.csv";
    string path = "/Tower";

    private void Start()
    {
        //SetConfigurationFromText();
        
        base.Start();
    }

    public override string GetConfigurationFromText()
    {
        // read and save configuration data from file
        StreamReader input = null;
        try
        {
            input = File.OpenText(
                Path.Combine(
                    Application.streamingAssetsPath + path,
                    ConfigurationDataFileName
                ));

            string names = input.ReadLine();
            string values = input.ReadLine();



            return values;
        }
        catch (Exception e)
        {
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

}
