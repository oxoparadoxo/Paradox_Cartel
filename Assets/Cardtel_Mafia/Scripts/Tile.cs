using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public TMP_Text _TextMesh;
    
    public void SetText(string text)
    {
        if(_TextMesh != null)
        {
            _TextMesh.text = text;
        }
    }
    
}
