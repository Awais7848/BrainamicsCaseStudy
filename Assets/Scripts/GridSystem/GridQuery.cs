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
            if (gridSystem.IsValidPosition(newPos))
                neighbors.Add(newPos);
        }
        return neighbors;
    }



}
