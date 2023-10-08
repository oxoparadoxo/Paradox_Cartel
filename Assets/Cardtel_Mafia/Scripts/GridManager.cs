using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public GameObject _TilePrefab;
    public Vector3 _InstaPosition;
    public int _Width,_Height,_Resolution;
    public bool _Floor = false;
    public int randomNumber = 0;
    public int Row = 0;
    public int Column = 0;
    public List<int> GridNumbers = new List<int>();
    public List<int> RandomNumbers = new List<int>();
    public int PairCounter = 1;
    public int UniqueIndex = 0;
    public GameObject _InstantiatedTile;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UniqueIndex = 1;
        _InstaPosition = Vector3.zero;
    }

    public void PlayGame(int width,int height,bool IsFloor)
    {
        _Width = width;
        _Height = height;
        _Floor = IsFloor;
        CheckValidInput();
        GenerateList();
        GenerateGrid();
    }
    void GenerateList()
    {
        CheckValidInput();
        for (int i = 0; i < _Width*_Height; i++)
        {
            randomNumber = Random.Range(0, (_Width*_Height));
            Debug.Log(randomNumber);
            
            while (RandomNumbers.Contains(randomNumber)) 
            {
                randomNumber = Random.Range(0, (_Width * _Height)); 
            }
            RandomNumbers.Add(randomNumber);
            if(UniqueIndex > 0 && UniqueIndex < (((_Width*_Height)/2)+1))
            { 
                //Level.Add(RandomNumbers[i], UniqueIndex);
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }
            else
            {
                UniqueIndex = 1;
                //Level.Add(RandomNumbers[i], UniqueIndex);
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }    
        }
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.SetLevelComplete((_Width * _Height) / 2);
        }
    }
    void GenerateGrid()
    {
        CheckValidInput();
        if(_TilePrefab == null)
        {
            return;
        }

        _InstaPosition = Vector3.zero;
        for (int Row = 0; Row < _Height; Row++)
        {
            
            for (int Column = 0; Column < _Width; Column++)
            {
                _InstaPosition = new Vector3(Column * 1.5f, 0, Row*1.5f);
                _InstantiatedTile = Instantiate(_TilePrefab, _InstaPosition, Quaternion.identity);
                _InstantiatedTile.transform.position = new Vector3(_InstaPosition.x, _TilePrefab.transform.position.y, _InstaPosition.z);
                _InstantiatedTile.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                _InstantiatedTile.transform.name = GridNumbers[RandomNumbers[Row * _Width + Column]].ToString();
                _InstantiatedTile.GetComponent<Tile>()?.SetText(GridNumbers[RandomNumbers[Row * _Width + Column]].ToString());
                _InstantiatedTile.transform.parent = gameObject.transform;
                //Debug.Log("Row : " + Row + " & Column : " + Column);
            }
        }
    }

    void CheckValidInput()
    {
        if(_Width == 0)
        {
            _Width = 2;
        }

        if(_Height == 0)
        {
            _Height = 2;
        }

        bool IsGreater = false;
        while (_Width*_Height % 2 != 0)
        {
            if (_Floor)
            {
                IsGreater = (_Width > _Height);

                if (!IsGreater && _Height % 2 != 0)
                {
                    if (_Height > 2)
                    {
                        _Height--;
                    }
                    else if (_Height < 2)
                    {
                        _Height++;
                    }
                    else
                    {
                        _Height = 2;
                    }
                }
                else if (IsGreater && _Width % 2 != 0)
                {
                    if (_Width > 2)
                    {
                        _Width--;
                    }
                    else if (_Width < 2)
                    {
                        _Width++;
                    }
                    else
                    {
                        _Width = 2;
                    }
                }
            }
            else
            {
                IsGreater = (_Width > _Height);
                if (IsGreater && _Height % 2 != 0)
                {
                    if (_Height > 2)
                    {
                        _Height++;
                    }
                    else if (_Height < 2)
                    {
                        _Height++;
                    }
                    else
                    {
                        _Height = 2;
                    }
                }
                else if (!IsGreater && _Width % 2 != 0)
                {
                    if (_Width > 2)
                    {
                        _Width++;
                    }
                    else if (_Width < 2)
                    {
                        _Width++;
                    }
                    else
                    {
                        _Width = 2;
                    }
                }
            }
           
        }
        SetCameraPosition(_Width, _Height);
    }

    void SetCameraPosition(float width,float height)
    {
        float up = (width>height)? width : height;
        Camera.main.transform.position = new Vector3((float)(width/2), up *2f, (float)(height/2));
    }


    public void ClearGame()
    {
        if(gameObject.transform.childCount >0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
               Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
        GridNumbers.Clear();
        RandomNumbers.Clear();
        UniqueIndex = 1;
        _InstaPosition = Vector3.zero;
    }
}
