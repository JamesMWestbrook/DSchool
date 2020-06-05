using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInfo : MonoBehaviour
{
    public Data Data;
    private string SavePath = "/data.json";


    public bool Save;
    public void Start()
    {
        Data.Items = GetComponent<Inventory>().Items;
        Data.CombatItems = GetComponent<Inventory>().CombatItems;

        if (Save)
        {
            SaveGameData();

        }
        else LoadGameData();
        // LoadGameData();
    }


    public void LoadGameData()
    {

        string filePath = Application.persistentDataPath + SavePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Data = JsonUtility.FromJson<Data>(dataAsJson);

        }
        else
        {
            SaveGameData();
            //Data = new Data();
        }
    }

    public void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(Data, true);
        string filePath = Application.persistentDataPath + SavePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}
