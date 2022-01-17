using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;
    [Tooltip("Cell size should be equal to grid multiplier.")]
    [SerializeField] float cellSize = 10f;
    [SerializeField] GameObject tilePrefab;
    Grid grid;

    private void Start()
    {
        GridCreator(width, height, cellSize);    
    }

    private void Update()
    {
        print(GetMouseWorldPosition());
    }

    private void GridCreator(int width, int height, float cellSize)
    {
        Grid grid = new Grid(width, height, cellSize);

        for (int i=0; i<width; i++)
        {
            for (int j=0; j<height; j++)
            {
                CreateSingleTile(i, j, cellSize, tilePrefab);
            }
        }
    }

    private GameObject CreateSingleTile(int posX, int posZ, float cellSize, GameObject prefab)
    {
        GameObject gameObject = Instantiate(prefab, new Vector3(posX * cellSize, 0.5f, posZ * cellSize), Quaternion.identity) as GameObject;
        gameObject.transform.parent = GameObject.Find("Grid").transform;
                
        gameObject.AddComponent<TileProps>();
        TileProps tileProps = gameObject.GetComponent<TileProps>();
        tileProps.tilePrefab = tilePrefab;

        return gameObject;
    }

    public Vector3 GetMouseWorldPosition()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hasHit = Physics.Raycast(ray, out raycastHit);

        int x ,y ,z;
        x = Mathf.FloorToInt((raycastHit.point.x + 5) / 10);
        y = Mathf.FloorToInt(raycastHit.point.y);
        z = Mathf.FloorToInt((raycastHit.point.z + 5) / 10);

        Vector3 vec = new Vector3(x,y,z);

        if (hasHit && raycastHit.transform.tag == "Tile")
        {
            if (Input.GetMouseButtonDown(0))
            {
                print(raycastHit.transform.name);
                GameObject cube = DrawObject(vec);

                vec *= 10;
                vec.y = 1f;
                cube.transform.position = vec;
            }
        }

        return vec;
    }

    private GameObject DrawObject(Vector3 placeToDraw)
    {
        // Changing grid coordinates to cell size scale coordinates
        placeToDraw*=cellSize;
        placeToDraw.y = 1f;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
        cube.GetComponent<Renderer>().material.color = Color.black;
        return cube;
    }
}
