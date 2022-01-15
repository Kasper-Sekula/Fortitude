using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateManager : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();        
    }

    private void Update()
    {
        DisplayTileCoordinates();
        ChangeTileName();    
    }
    
    private void DisplayTileCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10);
        label.text = coordinates.x + " , " + coordinates.y;
    }

    private void ChangeTileName()
    {
        transform.parent.name = coordinates.x + " , " + coordinates.y;
    }
}
