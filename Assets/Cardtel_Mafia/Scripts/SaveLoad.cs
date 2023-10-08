using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public string _Name;
    public int _Score;
    public int _Tries;
}
public class SaveLoad : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;

    void Start()
    {
        playerData = new PlayerData();
        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.D))
            DeleteSaveFile();

        
    }

    public void SaveGame()
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        }
        else
        {
            Debug.Log("There is no save files to load!");
        }

    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}



