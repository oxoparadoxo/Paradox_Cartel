using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public RegisteredPlayers players;
    public PlayerData playerData;
    public GameObject UIRoot;
    public List<GameObject> Menu = new List<GameObject>();

    public GameObject Level;
    public TMP_InputField m_Rows;
    public TMP_InputField m_Columns;
    public Toggle m_IsFloor;
    public bool _IsFloor;
    int _Rows;
    int _Columns;

    public TMP_Text m_ScoreText;
    public TMP_Text m_TriesText;

    public TMP_InputField m_RegisterUsername;
    public TMP_InputField m_RegisterPassword;

    public TMP_InputField m_LoginUsername;
    public TMP_InputField m_LoginPassword;

    public string m_CurrentUsername;
    public int m_CurrentScore;
    public int m_CurrentTries;
    public int m_CurrentUserLevelIndex;
    public int m_CurrentUserLevelRows;
    public int m_CurrentUserLevelColumns;
    public bool m_CurrentUserLevelFloor;
    public string RegisteredPlayersPath = "";
    public string PlayerDataPath = "";
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; 
        }
        
    }
    private void Start()
    {
        if (UIRoot != null)
        { 
            GetChildren(UIRoot);
            ActivateMenu("Splash");
            Invoke("ShowMainMenu", 1f);
            RegisteredPlayersPath = Application.dataPath + "/RegisteredPlayers.json";
            PlayerDataPath = Application.dataPath + "/PlayerSaveData.json";
        }
    }
    void GetChildren(GameObject root)
    {
        if (root != null && root.transform.childCount >0)
        {
            for (int i = 0; i < root.transform.childCount; i++)
            {
                Menu.Add(root.transform.GetChild(i).gameObject);
            }
        }
    }

    public void ActivateMenu(string name = "")
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio();
        }
        if (Menu.Count >0)
        {
            for (int i = 0; i < Menu.Count; i++)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (Menu[i].name.Contains(name))
                    {
                        Menu[i].SetActive(true);
                    }
                    else
                    {
                        Menu[i].SetActive(false);
                    }
                }
                else
                {
                    Menu[i].SetActive(false);
                }
            }
        }
    }

    void ShowMainMenu()
    {
        ActivateMenu("MainMenu");
    }

    
    public void PlayGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio();
        }
        _Rows = System.Convert.ToInt32(m_Rows.text);
        _Columns = System.Convert.ToInt32(m_Columns.text);
        _IsFloor = m_IsFloor.isOn;
        if (GridManager.Instance != null)
        {
            GridManager.Instance.PlayGame(_Rows, _Columns, _IsFloor);
        }
        ActivateMenu();
        SaveScore();
    }

    public void SetScoreTry(int score, int tries)
    {
        if(m_ScoreText != null)
        { 
            m_ScoreText.text = score.ToString();
        }

        if(m_TriesText != null)
        {
            m_TriesText.text = tries.ToString();
        }
        ActivateMenu("Congratulations");
    }

    public void SaveScore()
    {
        PlayerData playerData = new PlayerData(m_LoginUsername.text,m_ScoreText.text,m_TriesText.text,_Rows.ToString()+" X "+_Columns.ToString());
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(PlayerDataPath, savePlayerData);
    }

    public void LoadData()
    {
        PlayerData playerData;
        string loadPlayerData = File.ReadAllText(PlayerDataPath);
        playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
    }

    public void LoginPlayer()
    {
        if (File.Exists(RegisteredPlayersPath))
        {
            RegisteredPlayers loginPlayers = new RegisteredPlayers();
            string loadPlayers = File.ReadAllText(RegisteredPlayersPath);
            loginPlayers = JsonUtility.FromJson<RegisteredPlayers>(loadPlayers);
            if(!string.IsNullOrEmpty(m_LoginUsername.text) && !string.IsNullOrEmpty(m_LoginPassword.text))
            {
                if (loginPlayers.m_RegisteredPlayers[0].Username.Equals(m_LoginUsername.text) && loginPlayers.m_RegisteredPlayers[0].Password.Equals(m_LoginPassword.text))
                {
                    ActivateMenu("CustomLevelScreen");
                }
                else
                {
                    ActivateMenu("RegisterScreen");
                }
               
            }
        }
    }
    public void RegisterPlayer()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio();
        }
        if (!string.IsNullOrEmpty(m_RegisterUsername.text) && !string.IsNullOrEmpty(m_RegisterPassword.text))
        {
            RegisteredPlayers registeredPlayer = new RegisteredPlayers();
            registeredPlayer.m_RegisteredPlayers.Add(new LoginData(m_RegisterUsername.text, m_RegisterPassword.text));
            string savePlayers = JsonUtility.ToJson(registeredPlayer);
            File.WriteAllText(RegisteredPlayersPath, savePlayers);

        }
        
    }

    

    public void DeleteSaveFile()
    {
        if (File.Exists(RegisteredPlayersPath))
        {
            File.Delete(RegisteredPlayersPath);

            Debug.Log("Save file deleted!");
        }
        else
        {
            Debug.Log("There is nothing to delete!");
        }
    }
}
[System.Serializable]
public class LoginData
{
    public string Username;
    public string Password;

    public LoginData(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

public class RegisteredPlayers
{
    public List<LoginData> m_RegisteredPlayers = new List<LoginData>();
}


[System.Serializable]
public class PlayerData
{
    public string _Name;
    public string _Score;
    public string _Tries;
    public string _Level;
    public PlayerData(string name, string score, string tries, string level)
    {
        _Name = name;
        _Score = score;
        _Tries = tries;
        _Level = level;
    }
}