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

    private void GridCreator(int width, int height, float cellSize)
    {
        grid = new Grid(width, height, cellSize);

        for (int i=0; i<width; i++)
        {
            for (int j=0; j<height; j++)
            {
                GameObject gameObject = Instantiate(tilePrefab, new Vector3(i *10, 0.5f, j*10), Quaternion.identity) as GameObject;
                gameObject.transform.parent = GameObject.Find("Grid").transform;
            }
        }
    }

    private void Start()
    {
        GridCreator(width, height, cellSize);    
    }

    private void Update()
    {
        print(GetMouseWorldPosition());
    }

    public Vector3 GetMouseWorldPosition()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hasHit = Physics.Raycast(ray, out raycastHit);

        if (hasHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print(raycastHit.transform.name);  
            }
        }

        int x ,y ,z;
        x = Mathf.FloorToInt((raycastHit.point.x + 5) / 10);
        y = Mathf.FloorToInt(raycastHit.point.y);
        z = Mathf.FloorToInt((raycastHit.point.z + 5) / 10);

        Vector3 vec = new Vector3(x,y,z);
        return vec;
    }
}
