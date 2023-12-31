using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    
    public static void SaveGame(GameManager player)
    {
        // saves data of the player (including lvl, health, xp, etc)
        BinaryFormatter formatter = new BinaryFormatter();
        string path;
        path = Application.persistentDataPath + "/PlayerData.cba";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static PlayerData LoadPlayer()
    {
        // making the same variable again like a boss
        string path;
        path = Application.persistentDataPath + "/PlayerData.cba";


        if (File.Exists(path))
        {
            // load data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveUpgrades(Upgrades data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/UpgradeData.cba";
        FileStream stream = new FileStream(path, FileMode.Create);

        UpgradeData upgrade = new UpgradeData(data);

        formatter.Serialize(stream, upgrade);
        stream.Close();
    }

    public static UpgradeData LoadUpgrades()
    {
        // making the same variable again like a boss
        string path;
        path = Application.persistentDataPath + "/UpgradeData.cba";


        if (File.Exists(path))
        {
            // load data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            UpgradeData data = formatter.Deserialize(stream) as UpgradeData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    /*public static void SaveSettings(SystemSettings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.poo";
        FileStream stream = new FileStream(path, FileMode.Create);

        SystemData data = new SystemData(settings);

        formatter.Serialize(stream, data);
        stream.Close();
    } */

    /*public static SystemData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.poo";
        if (File.Exists(path))
        {
            // load data
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SystemData data = formatter.Deserialize(stream) as SystemData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    } */
   
    // check if a save file for the player exists and if it doesn't, return false 
    public static bool DoesPlayerFileExist()
    {
        string path = Application.persistentDataPath + "/PlayerData.cba";
        if (File.Exists(path))
        {
            Debug.Log("exists");
            return true;
        }
        else
        {
            Debug.Log("Doesn't exist for some reason");
            return false;
        }
    }

    public static bool DoesUpgradeFileExist()
    {
        string path = Application.persistentDataPath + "/UpgradeData.cba";
        if (File.Exists(path))
        {
            Debug.Log("exists");
            return true;
        }
        else
        {
            Debug.Log("Doesn't exist for some reason");
            return false;
        }
    }

    public static bool DoesSettingsFileExist()
    {
        string path = Application.persistentDataPath + "/settings.poo";
        if (File.Exists(path))
        {
            Debug.Log("exists");
            return true;
        }
        else
        {
            Debug.Log("Doesn't exist for some reason");
            return false;
        }
    }

    public static bool DoesStorageFileExist()
    {
        string path = Application.persistentDataPath + "/PlayerData.cba";

        if (File.Exists(path))
        {
            Debug.Log("StorageData exists");
            return true;
        }
        else
        {
            Debug.Log("StorageData doesn't exist");
            return false;
        }    
    }

    // initially create a player file with data and do nothing if there is one
    public static void CreatePlayerFile(GameManager player)
    {
        string path;


        path = Application.persistentDataPath + "/PlayerData.cba";

        if (!File.Exists(path))
        {
            Debug.Log("GameData doesn't exist, creating!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.Log("GameData exists, doing nothing!");
            return;
        }
    }

    public static void CreateUpgradeFile(Upgrades upgrades)
    {
        string path;


        path = Application.persistentDataPath + "/UpgradeData.cba";

        if (!File.Exists(path))
        {
            Debug.Log("UpgradeData doesn't exist, creating!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            UpgradeData data = new UpgradeData(upgrades);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.Log("UpgradeData exists, doing nothing!");
            return;
        }
    }

    /*public static void CreateSettingsFile(SystemSettings settings)
    {
        string path = Application.persistentDataPath + "/settings.poo";
        if (!File.Exists(path))
        {
            Debug.Log("GameData doesn't exist, creating!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            SystemData data = new SystemData(settings);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            Debug.Log("settings exists, doing nothing!");
            return;
        }
    } */

    public static void DeletePlayerFile()
    {

        string path;
        
        path = Application.persistentDataPath + "/PlayerData.cba";
        
        File.Delete(path);
    }

    public static void DeleteSettingsFile()
    {
        string path = Application.persistentDataPath + "/settings.poo";
        File.Delete(path);
    }

    public static void DeleteUpgradeFile()
    {
        string path;

        path = Application.persistentDataPath + "/UpgradeData.cba";

        File.Delete(path);
    }

}
