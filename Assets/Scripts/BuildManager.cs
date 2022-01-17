using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    float cellSize;

    private void Start()
    {
        cellSize = 10f;
    }

    private void Update()
    {
        //ClickToBuild(GetComponent<GridManager>().GetMouseWorldPosition);    
    }

    private Vector3 ClickToBuild(RaycastHit raycastHit, bool hasHit, Vector3 vec)
    {
        
        if (hasHit && raycastHit.transform.tag == "Tile")
        {
            if (Input.GetMouseButtonDown(0))
            {
                TileProps tileprops = raycastHit.transform.GetComponent<TileProps>();
                if (tileprops.CheckIfCanBuildOnTile())
                {
                    GameObject cube = DrawObject(vec);

                    vec *= 10;
                    vec.y = 1f;
                    cube.transform.position = vec;
                    print(tileprops.SoilType);
                }

            }
        }

        return vec;
    }

    private GameObject DrawObject(Vector3 placeToDraw)
    {
        // Changing grid coordinates to cell size scale coordinates
        placeToDraw *= cellSize;
        placeToDraw.y = 1f;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
        cube.GetComponent<Renderer>().material.color = Color.black;
        return cube;
    }
}
