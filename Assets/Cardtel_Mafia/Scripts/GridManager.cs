using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int _Width;
    public Dictionary<int,int> Level = new Dictionary<int,int>();
    public int randomNumber = 0;
    public int Row = 0;
    public int Column = 0;
    public List<int> GridNumbers = new List<int>();
    public int PairCounter = 1;
    private void Start()
    {
        GenerateGrid();
        GenerateList();
    }

    void GenerateList()
    {
        for (int i = 0; i < _Width*_Width; i++)
        {
            randomNumber = Random.Range(1, (_Width*_Width+1));
            Debug.Log(randomNumber);
            GridNumbers.Add(randomNumber);
        }
    }
    void GenerateGrid()
    {
        for (int Row = 0; Row < _Width; Row++)
        {
            for (int Column = 0; Column < _Width; Column++)
            {
                Debug.Log("Row : " + Row + " & Column : " + Column);
            }
        }
    }
}
