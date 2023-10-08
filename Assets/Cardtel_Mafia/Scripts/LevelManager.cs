using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject SelectedTile;
    public GameObject PairTile;
    public int PairCount = 0;
    public int Score = 0;
    public int Try = 0;
    // Start is called before the first frame update
    void Start()
    {
        PairCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SelectRotateTile();
        }

    }

    void SelectRotateTile()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(PairCount >0 && PairCount <3)
            {
                GameObject go = hit.collider.gameObject;
                if (go != null)
                {
                    Debug.Log(go.name);
                    go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if(PairCount == 1)
                {
                    SelectedTile = go;
                    PairCount++;
                }
                else if(PairCount == 2)
                {
                    PairTile = go;
                    PairCount++;
                }
                if (PairTile.gameObject != null && SelectedTile.gameObject != null)
                {
                    if (PairTile.name.Equals(SelectedTile.name))
                    {
                        CancelInvoke();
                        Invoke("DelayCorrectPair", 1f);
                    }
                    else
                    {
                        CancelInvoke();
                        Invoke("DelayWrongPair", 1f);
                    }
                }
            }
            else
            {
                PairCount = 1;
                GameObject go = hit.collider.gameObject;
                if (go != null)
                {
                    Debug.Log(go.name);
                    go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                if (PairCount == 1)
                {
                    SelectedTile = go;
                    PairCount++;
                }
            }
            
        }
    }

    void DelayCorrectPair()
    {
        if (PairTile.gameObject != null)
        {
            PairTile.SetActive(false);
        }
        if (SelectedTile.gameObject != null)
        {
            SelectedTile.SetActive(false);
        }
        
        Score++;
        Try++;
    }

    void DelayWrongPair()
    {
        if(PairTile.gameObject != null)
        {
            PairTile.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
        if(SelectedTile.gameObject != null)
        {
            SelectedTile.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
        }
        
        Try++;
    }
}
