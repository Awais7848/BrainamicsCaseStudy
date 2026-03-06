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

    public List<GridPosition> GetRow(GridPosition pos)
    {
        List<GridPosition> row = new List<GridPosition>();

        row.AddRange(ScanDirection(pos, GridDirections.Left));
        row.Add(pos);
        row.AddRange(ScanDirection(pos, GridDirections.Right));

        return row;
    }

    public List<GridPosition> GetColumn(GridPosition pos)
    {
        List<GridPosition> column = new List<GridPosition>();

        column.AddRange(ScanDirection(pos, GridDirections.Up));
        column.Add(pos);
        column.AddRange(ScanDirection(pos, GridDirections.Down));

        return column;
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
