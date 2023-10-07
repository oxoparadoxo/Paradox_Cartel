using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
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
    private void Start()
    {
        CheckValidInput();
        GenerateGrid();
        GenerateList();
        UniqueIndex = 1;

    }

    void GenerateList()
    {
        CheckValidInput();
        for (int i = 0; i < _Width*_Height; i++)
        {
            _Resolution = _Width * _Height;
            
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
        for (int Row = 0; Row < _Height; Row++)
        {
            for (int Column = 0; Column < _Width; Column++)
            {
                Debug.Log("Row : " + Row + " & Column : " + Column);
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

        while (_Resolution % 2 != 0)
        {
            if (_Width % 2 == 0)
            {
                if (_Floor && _Height > 2)
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
            else
            {
                if (_Floor && _Width > 2)
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
    }
}
