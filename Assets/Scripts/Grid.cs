using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject> {

    public EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private TGridObject[,] array;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPosition;

        array = new TGridObject[width, height];

        for (int x = 0; x < array.GetLength(0); x++){
            for (int z = 0; z < array.GetLength(1); z++){
                array[x,z] = createGridObject(this, x, z);
            }
        }

        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < array.GetLength(0); x++) {
                for (int z = 0; z < array.GetLength(1); z++) {
                    debugTextArray[x, z] = CreateWorldText(array[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * .5f, 15, Color.black, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.black, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.black, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.z].text = array[eventArgs.x, eventArgs.z]?.ToString();
            };
        }

    }

    public int Width{
        get => _width;
    }

    public int Height{
        get => _height;
    }

    public float CellSize{
        get => _cellSize;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3( x, 0, z) * _cellSize + _originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject gridObject)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            array[x,z] = gridObject;
            TriggerGridObjectChanged(x,z);
        }
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z});
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject gridObject)
    {
        GetXZ(worldPosition, out int x, out int z);
        SetGridObject(x, z, gridObject);
    }

    public TGridObject GetGridObject(int x, int z) {
        if (x >= 0 && z >= 0 && x < _width && z < _height) {
            return array[x, z];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000) {
        if (color == null) color = Color.black;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
        
    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    } 
}

