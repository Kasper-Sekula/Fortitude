using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    public static PlacedBuilding Create(Vector3 worldPosition, Vector2Int origin, BuildingSO.Dir currentDir, BuildingSO buildingSO)
    {
        Transform placedBuildingTransform = Instantiate(buildingSO.prefab, worldPosition, Quaternion.Euler(0, buildingSO.GetRotationAngle(currentDir), 0));

        PlacedBuilding placedBuilding = placedBuildingTransform.GetComponent<PlacedBuilding>();

        placedBuilding.placedBuildingSO = buildingSO;
        placedBuilding.origin = origin;
        placedBuilding.currentDir = currentDir;

        return placedBuilding;
    }

    BuildingSO placedBuildingSO;
    Vector2Int origin;
    BuildingSO.Dir currentDir;

    public List<Vector2Int> GetGridPositionList(){
        return placedBuildingSO.GetGridPositionList(origin, currentDir);
    }

    public void DestroyThis(){
        Destroy(gameObject);
    }
}
