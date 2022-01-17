using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProps : MonoBehaviour
{
    bool _canBuild = true;
    public GameObject tilePrefab;
    string _soilType = "Desert";

    public bool CheckIfCanBuildOnTile()
    {
        if (_canBuild) { return true; }
        return false;
    }

    public string SoilType
    {
        get => _soilType;
        set => _soilType = value;
    }

}
