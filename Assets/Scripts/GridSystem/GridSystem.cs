using System.Collections.Generic;
using UnityEngine;

public class GridSystem<T>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 origin;
    private GridNode<T>[,] grid;

    public GridSystem(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;

        grid = new GridNode<T>[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new GridNode<T>(new GridPosition(x, y));

                DrawLine(new Vector3(grid[x,y].Position.X, 0F, grid[x, y].Position.Y) + origin, new Vector3(grid[x, y].Position.X, 0F, grid[x, y].Position.Y + 1) + origin);

            DrawLine(new Vector3(grid[x, y].Position.X, 0F, grid[x, y].Position.Y) + origin, new Vector3(grid[x, y].Position.X + 1, 0F, grid[x, y].Position.Y) + origin);


            }

            DrawLine(new Vector3(width, 0F, 0) + origin, new Vector3(width, 0F, height)+origin);
           DrawLine(new Vector3(0f, 0F, height) + origin, new Vector3(width, 0F, height)+origin);
       


    }


      void DrawLine(Vector3 startPosition, Vector3 endPosition)
    {
        Debug.DrawLine(startPosition, endPosition, Color.black, 100f);
    }
   
    public bool IsValidPosition(GridPosition pos)
        => pos.X >= 0 && pos.X < width && pos.Y >= 0 && pos.Y < height;

    public GridNode<T> GetNode(GridPosition pos)
        => IsValidPosition(pos) ? grid[pos.X, pos.Y] : null;

    public void SetItem(GridPosition pos, T item)
    {
        if (!IsValidPosition(pos)) return;
        grid[pos.X, pos.Y].SetItem(item);
    }

    public T GetItem(GridPosition pos)
    {
        if (!IsValidPosition(pos)) return default;
        return grid[pos.X, pos.Y].GetItem();
    }

    public bool IsEmpty(GridPosition pos)
    {
        if (!IsValidPosition(pos)) return default;
        return grid[pos.X, pos.Y].IsEmpty;
    }


    public void RemoveItem(GridPosition pos)
    {
        if (!IsValidPosition(pos)) return;
        grid[pos.X, pos.Y].Clear();
    }





    public GridPosition WorldToGrid(Vector3 worldPos, bool is3D = true)
    {
        Vector3 relative = worldPos - origin;
        int x = Mathf.FloorToInt(relative.x / cellSize);
        int y = is3D ? Mathf.FloorToInt(relative.z / cellSize) : Mathf.FloorToInt(relative.y / cellSize);
        return new GridPosition(x, y);
    }

    public Vector3 GridToWorld(GridPosition pos, bool is3D = true)
    {
        float x = origin.x + pos.X * cellSize + cellSize / 2f;
        float z_or_y = origin.z + pos.Y * cellSize + cellSize / 2f;
        return is3D ? new Vector3(x, origin.y, z_or_y) : new Vector3(x, z_or_y, origin.z);
    }
}