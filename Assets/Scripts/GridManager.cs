using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GridManager : MonoBehaviour
{
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;
    [Tooltip("Cell size should be equal to grid multiplier.")]
    [SerializeField] float cellSize = 10f;
    [SerializeField] GameObject mainTreePrefab;
    GameObject[,] arr;
    Grid<GridObject> grid;

    BuildingSO buildingSO;
    [SerializeField] List<BuildingSO> listOfBuidlings;
    BuildingSO.Dir currentDir = BuildingSO.Dir.Down;
    [SerializeField] List<Button> buttons;
    Transform preview;    
    float incomePerSecond = 0f;
    float amount = 100f;
    protected float resourceTimer;

    private void Awake()
    {
        grid = new Grid<GridObject>(width,height,cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));   
        
        buildingSO = null;
    }

    private void Start()
    {
        //GridCreator(width, height, cellSize);
        CreateMainTree();
        //preview = Instantiate(buildingSO.prefab, GetMouseWorldPosition(), Quaternion.identity);
    }

    private void Update()
    {
        CheckPreviewPosition();

        // Unable to clicl through UI buttons
        bool clickOnButton = false;
        foreach (Button button in buttons)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(button.GetComponent<RectTransform>(), Input.mousePosition)) { clickOnButton = true; }
        }

        // Left mouse click
        if (Input.GetMouseButtonDown(0) && !clickOnButton)
        {
            Vector3 vec = GetMouseWorldPosition();
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);

            List<Vector2Int> gridPositionList = buildingSO.GetGridPositionList(new Vector2Int(x, z), currentDir);

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
                Vector2Int rotationOffset = buildingSO.GetRotationOffset(currentDir);
                Vector3 buildingWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.CellSize;

                if (amount >= buildingSO.costToBuild)
                {
                    amount -= buildingSO.costToBuild;
                    PlacedBuilding placedBuilding = PlacedBuilding.Create(buildingWorldPosition, new Vector2Int(x, z), currentDir, buildingSO);

                    // Saving seed type to PlacedBuilding
                    placedBuilding.SetSeedType(buildingSO.seedType);    
                    // Add Income when building is placed
                    incomePerSecond += placedBuilding.GetIncome();
                    print($"{placedBuilding.name} added, now income: {incomePerSecond}");

                    // Transform buildTransform = Instantiate(buildingSO.prefab, buildingWorldPosition, Quaternion.Euler(0, buildingSO.GetRotationAngle(currentDir), 0));

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedbuilding(placedBuilding);
                    }
                    //gridObject.SetTransform(buildTransform);
                }
                else {
                    print ($"Amount is too low to build: {amount} {buildingSO.costToBuild}");
                }


            }
            else
            {
                print("Cannot build here!");
            }
        }

        // Demolish
        if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = grid.GetGridObject(GetMouseWorldPosition());
            PlacedBuilding placedBuilding = gridObject.GetPlacedBuilding();
            if (placedBuilding != null)
            {
                placedBuilding.DestroyThis();

                List<Vector2Int> gridPositionList = placedBuilding.GetGridPositionList();
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
                incomePerSecond -= placedBuilding.GetIncome();
                print(placedBuilding.GetIncome());
                print(incomePerSecond);
                amount += (buildingSO.costToBuild / 2);
            }
        }

        // Building rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentDir = BuildingSO.GetNextDir(currentDir);
        }

        // Building choice
        if (Input.GetKeyDown(KeyCode.Alpha1)) { buildingSO = listOfBuidlings[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { buildingSO = listOfBuidlings[1]; }

        CalculateIncome();
    }

    private void CalculateIncome()
    {
        resourceTimer += Time.deltaTime;

        if (resourceTimer >= 1)
        {
            resourceTimer = 0f;
            amount += incomePerSecond;
        }
        //income = income * Time.deltaTime;
        print(amount);
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

    public void ChooseBuildingToBuild(int i)
    {
        if (i == 9)
        {
            // TODO Cancel choice
            print("9");
        }
        else
        {
            buildingSO = listOfBuidlings[i];
            if (preview != null) {
                Destroy(preview.gameObject);
                SetPreview();
            } else{
                SetPreview();
            }  
        }
    }

    void CheckPreviewPosition()
    {
        if (buildingSO == null) { return; }
        Vector2Int rotationOffset = buildingSO.GetRotationOffset(currentDir);
        grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
        Vector3 vec = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.CellSize;
        //Vector3 vec = GetMouseWorldPosition();
        vec.y = 0;
        if (preview.transform.position != vec)
        {
            Destroy(preview.gameObject);
            preview = Instantiate(buildingSO.prefab, vec, Quaternion.Euler(0, buildingSO.GetRotationAngle(currentDir), 0));
        }
    }

    void SetPreview()
    {
        preview = Instantiate(buildingSO.prefab, GetMouseWorldPosition(), Quaternion.identity);
    }


    public List<BuildingSO> GetListOfBuildings(){
        return listOfBuidlings;
    }
}
