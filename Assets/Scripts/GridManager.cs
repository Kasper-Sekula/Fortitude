using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;
    [SerializeField] GameObject tilePrefab;
    Grid grid;

    private void GridCreator(int width, int height)
    {
        grid = new Grid(width, height);

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
        GridCreator(width, height);    
    }
}
