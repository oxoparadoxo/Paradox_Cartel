using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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
            //ActivateMenu();
            ActivateMenu("Splash");
            Invoke("ShowMainMenu", 1f);
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
        if(Menu.Count >0)
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
        _Rows = System.Convert.ToInt32(m_Rows.text);
        _Columns = System.Convert.ToInt32(m_Columns.text);
        _IsFloor = m_IsFloor.isOn;
        if (GridManager.Instance != null)
        {
            GridManager.Instance.PlayGame(_Rows, _Columns, _IsFloor);
        }
        ActivateMenu();
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
}
