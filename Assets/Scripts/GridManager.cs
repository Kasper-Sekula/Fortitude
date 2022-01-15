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
        GetMouseWorldPosition();
    }

    public void GetMouseWorldPosition()
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

        Vector3 vec = raycastHit.point;
        print(vec);
    }
}
