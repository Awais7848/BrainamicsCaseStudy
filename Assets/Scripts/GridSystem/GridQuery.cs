using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridQuery<T>
{
    private GridSystem<T> gridSystem;

    public GridQuery(GridSystem<T> grid)
        {

        gridSystem = grid;

        }


    public List<GridPosition> GetNeighbors(GridPosition pos, GridPosition[] directions)
    {
        List<GridPosition> neighbors = new List<GridPosition>();


        foreach (var dir in directions)
        {
            GridPosition newPos = new GridPosition(pos.X + dir.X, pos.Y + dir.Y);
            if (gridSystem.IsValidPosition(newPos)&&!gridSystem.IsEmpty(newPos))
                neighbors.Add(newPos);
        }
        return neighbors;
    }

    public List<GridPosition> GetCurrentRow(GridPosition pos) {

        List<GridPosition> neighbors = new List<GridPosition>();
       
        for(int x = pos.X;x<=gridSystem.width;x++)

        {
            GridPosition newPos = new GridPosition(x, pos.Y);
            if (gridSystem.IsValidPosition(newPos) && !gridSystem.IsEmpty(newPos))
                neighbors.Add(newPos);

        }
        return neighbors;
    }
    public List<GridPosition> GetCurrentColumn(GridPosition pos)
    {

        List<GridPosition> neighbors = new List<GridPosition>();

        for (int y = pos.Y; y <= gridSystem.height; y++)

        {
            GridPosition newPos = new GridPosition(pos.X, y);
            if (gridSystem.IsValidPosition(newPos) && !gridSystem.IsEmpty(newPos))
                neighbors.Add(newPos);

        }
        return neighbors;
    }

    public List<GridPosition> ScanDirection(
    GridPosition start,
    GridPosition direction)
    {
        List<GridPosition> results = new List<GridPosition>();

        GridPosition current = start + direction;

        while (gridSystem.IsValidPosition(current))
        {
            if (gridSystem.IsEmpty(current))
                break;

            results.Add(current);

            current += direction;
        }

        return results;
    }

}
