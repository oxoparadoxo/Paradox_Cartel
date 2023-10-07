using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject _TilePrefab;
    public Vector3 _StartPosition;
    public int _Width,_Height,_Resolution;
    public bool _Floor = false;
    public Dictionary<int,int> Level = new Dictionary<int,int>();
    public int randomNumber = 0;
    public int Row = 0;
    public int Column = 0;
    public List<int> GridNumbers = new List<int>();
    public List<int> RandomNumbers = new List<int>();
    public int PairCounter = 1;
    public int UniqueIndex = 0;
    public GameObject _InstantiatedTile;
    private void Start()
    {
        CheckValidInput();
        GenerateList();
        GenerateGrid();
        UniqueIndex = 1;
        _StartPosition = Vector3.zero;

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
                Level.Add(RandomNumbers[i], UniqueIndex);
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }
            else
            {
                UniqueIndex = 1;
                Level.Add(RandomNumbers[i], UniqueIndex);
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }    
        }
    }
    void GenerateGrid()
    {
        CheckValidInput();
        if(_TilePrefab == null)
        {
            return;
        }

        for (int Row = 0; Row < _Height; Row++)
        {
            for (int Column = 0; Column < _Width; Column++)
            {
                _InstantiatedTile = Instantiate(_TilePrefab, _TilePrefab.transform.position, Quaternion.identity);
                _InstantiatedTile.transform.position = new Vector3(Column, _TilePrefab.transform.position.y, Row);
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
        Camera.main.transform.position = new Vector3((float)(width/2) - 0.5f, up + 1f, (float)(height/2) - 0.5f);
    }
}
