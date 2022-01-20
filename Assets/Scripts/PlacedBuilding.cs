using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedBuilding : MonoBehaviour
{
    public static PlacedBuilding Create(Vector3 worldPosition, Vector2Int origin, BuildingSO.Dir currentDir, BuildingSO buildingSO)
    {
        Transform placedBuildingTransform = Instantiate(buildingSO.prefab, worldPosition, Quaternion.Euler(0, buildingSO.GetRotationAngle(currentDir), 0));
        placedBuildingTransform.gameObject.AddComponent<PlacedBuilding>();

        PlacedBuilding placedBuilding = placedBuildingTransform.GetComponent<PlacedBuilding>();
        print(placedBuildingTransform.GetComponents(typeof(Component)));


        placedBuilding.placedBuildingSO = buildingSO;
        placedBuilding.origin = origin;
        placedBuilding.currentDir = currentDir;

        return placedBuilding;
    }

    BuildingSO placedBuildingSO;
    Vector2Int origin;
    BuildingSO.Dir currentDir;
    string seedType;
    float income = 2.0f;

    public List<Vector2Int> GetGridPositionList(){
        return placedBuildingSO.GetGridPositionList(origin, currentDir);
    }

    public void DestroyThis(){
        Destroy(gameObject);
    }

    public void SetSeedType(string seedType){
        this.seedType = seedType;
    }

    public float GetIncome()
    {
        return incomeDict[seedType];
    }

    Dictionary<string, float> incomeDict = new Dictionary<string, float>(){
        {"spruce", 2.0f},
        {"birch", 4.0f},
        {"oak", 6.0f}
    };
}
