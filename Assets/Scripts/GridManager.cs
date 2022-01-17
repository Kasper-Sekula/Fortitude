using System;
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
    [SerializeField] GameObject mainTreePrefab;
    GameObject[,] arr;
    private Grid<GridObject> grid;

    private void Awake()
    {
        grid = new Grid<GridObject>(width,height,cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));   
        print(grid);
    }

    public class GridObject {
        private Grid<GridObject> grid;
        private int x;
        private int z;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
    }
    

    private void Start()
    {
        //GridCreator(width, height, cellSize);
        CreateMainTree();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = GetMouseWorldPosition();
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
            Instantiate(tilePrefab, grid.GetWorldPosition(x,z), Quaternion.identity);
        }
    }

    void CreateMainTree()
    {
        GameObject gameObject = Instantiate(mainTreePrefab, new Vector3(width * cellSize / 2f, 0.5f, height * cellSize / 2f), Quaternion.identity) as GameObject;
    }

    public Vector3 GetMouseWorldPosition()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out raycastHit);
        if (hasHit) { return raycastHit.point; }
        else { return Vector3.zero; }
    }
}
