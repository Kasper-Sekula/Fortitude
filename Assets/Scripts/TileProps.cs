using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProps : MonoBehaviour
{
    bool canBuild = true;
    public GameObject tilePrefab;

    public bool CheckIfCanBuildOnTile()
    {
        if (canBuild) { return true; }
        return false;
    }

}
