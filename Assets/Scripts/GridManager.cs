using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class GridManager : MonoBehaviour
{
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;
    [Tooltip("Cell size should be equal to grid multiplier.")]
    [SerializeField] float cellSize = 10f;
    [SerializeField] GameObject mainTreePrefab;
    [SerializeField] BuildingSO buildingSO;
    GameObject[,] arr;
    private Grid<GridObject> grid;

    private void Awake()
    {
        grid = new Grid<GridObject>(width,height,cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));   
        print(grid);
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

            List<Vector2Int> gridPositionList = buildingSO.GetGridPositionList(new Vector2Int(x, z));

            bool canBuild = true;

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Transform buildTransform = Instantiate(buildingSO.prefab, grid.GetWorldPosition(x,z), Quaternion.identity);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(buildTransform);
                }

                //gridObject.SetTransform(buildTransform);
            } else
            {
                print("Cannot build here!");
            }
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
