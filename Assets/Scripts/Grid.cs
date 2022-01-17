using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    
    private int _width;
    private int _height;
    private float _cellSize;
    private GameObject[,] array;

    public Grid(int width, int height, float cellSize)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;

        array = new GameObject[width, height];
    }

    public void AddToGrid(int x, int y, GameObject value)
    {
        array[x,y] = value;
    }

}
