using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




public class SaveLoad : MonoBehaviour
{

    public static SaveLoad Instance;

    PlayerData playerData;
    string saveFilePath;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
   

    void Start()
    {
        
        
    }

    public void SetPlayerData(PlayerData playerData)
    {

    }


    
}



