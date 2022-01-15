using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;

        gridArray = new int[width, height];
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y) * _cellSize;
    }

    public void GetXY(Vector3 wordlPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(wordlPosition.x / _cellSize);
        y = Mathf.FloorToInt(wordlPosition.y / _cellSize);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y <_height) { return gridArray[x,y]; }
        else { return 0;}
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
