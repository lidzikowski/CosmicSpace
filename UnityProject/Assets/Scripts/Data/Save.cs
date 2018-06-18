using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static PlayerShips StartPlayerShip = PlayerShips.Avalon;


    public static void SavePlayer(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + Path.AltDirectorySeparatorChar + player.Username + ".dat", FileMode.OpenOrCreate);
        bf.Serialize(file, new SaveData(player));
        file.Close();
    }

    public static SaveData LoadPlayer(string username)
    {
        Debug.Log(Application.persistentDataPath + Path.AltDirectorySeparatorChar + username + ".dat");
        SaveData save;
        if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + username + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.AltDirectorySeparatorChar + username + ".dat", FileMode.Open);
            save = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            save = new SaveData();
        }
        return save;
    }

    public static void CheckSave(Player player)
    {
        if (!File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + player.Username + ".dat"))
            SavePlayer(player);
    }
}