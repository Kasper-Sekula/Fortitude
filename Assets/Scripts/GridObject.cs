using UnityEngine;

public class GridObject {
    private Grid<GridObject> grid;
    private int x;
    private int z;
    private Transform transform;

    public GridObject(Grid<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x,z);
    }

    public void ClearTransform()
    {
        transform = null;
        grid.TriggerGridObjectChanged(x,z);
    }

    public bool CanBuild()
    {
        return transform == null;
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + transform;
    }
}

