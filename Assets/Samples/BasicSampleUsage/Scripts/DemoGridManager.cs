using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;


[System.Serializable]
public struct GridSettings
{
    public int Width, Height;
   public float cellSize;
    public Vector3 origin;

  

}

public static class GridDebug
{
    public static void Log(string message)
    {
        Debug.Log("<color=green>[Debug]"+message+"</color>");
    }

    public static void Error(string message)
    {
        Debug.Log("<color=red>[Debug]" + message + "</color>");
    }
}



public class DemoGridManager : MonoBehaviour
{
    GridSystem<ColorBlock> sampleGrid;
    GridQuery<ColorBlock> sampleGridQuery;
   
    [SerializeField]List<GridSettings> gridSettings;

    GridSettings currentGrid;

    public bool Debug;

    private static DemoGridManager instance;

    private void Awake()
    {
        instance = this;
        currentGrid = gridSettings[0];
        SpawnGrid(gridSettings[0]);
    }
     void SpawnGrid(GridSettings gridSettings)
    {
        GridSystem<ColorBlock> gridSystem = new GridSystem<ColorBlock>(gridSettings.Width, gridSettings.Height, gridSettings.cellSize, gridSettings.origin);
        GridQuery<ColorBlock> gridQuery = new GridQuery<ColorBlock>(gridSystem);

        sampleGrid = gridSystem;
        sampleGridQuery = gridQuery;
    }

    public static void SpawnGridWithIndex(int settingIndex)
    {
        if (settingIndex >= instance.gridSettings.Count)
        {
            GridDebug.Error("No Settings Found For This Index");
            return;
        }
        instance.currentGrid = instance.gridSettings[settingIndex];
        instance.SpawnGrid(instance.currentGrid);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int x = 0; x < currentGrid.Width; x++)
            for (int y = 0; y < currentGrid.Height; y++)
            {
                Gizmos.DrawLine(new Vector3(x * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin, (new Vector3(x * currentGrid.cellSize, 0F, y + 1) * currentGrid.cellSize) + currentGrid.origin);

                Gizmos.DrawLine(new Vector3(x * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin, (new Vector3((x + 1) * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin));


            }

        Gizmos.DrawLine(new Vector3(currentGrid.Width, 0F, 0) * currentGrid.cellSize + currentGrid.origin, new Vector3(currentGrid.Width, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin);
        Gizmos.DrawLine(new Vector3(0f, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin, new Vector3(currentGrid.Width, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin);




    }

    public void ClearGrid()
    {
        for (int i = 0; i < sampleGrid.width; i++)
        {
            for(int j=0;j<sampleGrid.height; j++)
            {
                GridPosition gridPosition;
                gridPosition.X = i;
                gridPosition.Y = j;
                sampleGrid.RemoveItem(gridPosition);

            }

        }
    }

    public void SpawnGrid()
    {

    }


}
