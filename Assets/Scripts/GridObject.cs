using UnityEngine;

public class GridObject {
    private Grid<GridObject> grid;
    private int x;
    private int z;
    private PlacedBuilding placedBuilding;

    public GridObject(Grid<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetPlacedbuilding(PlacedBuilding placedBuilding)
    {
        this.placedBuilding= placedBuilding;
        grid.TriggerGridObjectChanged(x,z);
    }

    public PlacedBuilding GetPlacedBuilding(){
        return placedBuilding;
    }

    public void ClearPlacedObject()
    {
        placedBuilding = null;
        grid.TriggerGridObjectChanged(x,z);
    }

    public bool CanBuild()
    {
        return placedBuilding == null;
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + placedBuilding;
    }
}

