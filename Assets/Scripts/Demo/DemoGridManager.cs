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
        Debug.Log("[Debug] "+message);
    }

    public static void Error(string message)
    {
        Debug.Log("<color=red>[Debug] " + message + "</color>");
    }

    public static void Assert(string message)
    {
        Debug.Log("<color=yellow>[Debug] " + message + "</color>");

    }
}



public class DemoGridManager : MonoBehaviour
{
    GridSystem<ColorBlock> sampleGrid;
    GridQuery<ColorBlock> sampleGridQuery;

   public static GridSystem<ColorBlock> Grid => instance.sampleGrid;
   public static GridQuery<ColorBlock> GridQuery => instance.sampleGridQuery;





    [SerializeField]List<GridSettings> gridSettings;

    GridSettings currentGridSettings;

    public bool Debug;

    private static DemoGridManager instance;

    private void Awake()
    {
        instance = this;
        currentGridSettings = gridSettings[0];
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
       instance. ClearAndDestroyAllGridElements();
        instance.currentGridSettings = instance.gridSettings[settingIndex];
        instance.SpawnGrid(instance.currentGridSettings);

    }


  
    public void ClearAndDestroyAllGridElements()
    {
        if (sampleGrid == null)
        {
            GridDebug.Error("Grid not initialized. Cannot clear elements.");
            return;
        }

        for (int x = 0; x < sampleGrid.width; x++)
        {
            for (int y = 0; y < sampleGrid.height; y++)
            {
                GridPosition pos = new GridPosition(x, y);

                if (!sampleGrid.IsEmpty(pos))
                {
                    var element = sampleGrid.GetItem(pos); 
                    if (element != null)
                    {
                      
                          Destroy(element.gameObject);
                       
                    }

                    sampleGrid.RemoveItem(pos);
                }
            }
        }

        GridDebug.Log("All grid elements removed and destroyed.");
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (!Debug)
            return;

            Gizmos.color = Color.black;

            for (int x = 0; x <= currentGridSettings.Width; x++)
            {
                Vector3 start = new Vector3(x * currentGridSettings.cellSize, 0, 0) + currentGridSettings.origin;
                Vector3 end = new Vector3(x * currentGridSettings.cellSize, 0, currentGridSettings.Height * currentGridSettings.cellSize) + currentGridSettings.origin;

                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= currentGridSettings.Height; y++)
            {
                Vector3 start = new Vector3(0, 0, y * currentGridSettings.cellSize) + currentGridSettings.origin;
                Vector3 end = new Vector3(currentGridSettings.Width * currentGridSettings.cellSize, 0, y * currentGridSettings.cellSize) + currentGridSettings.origin;

                Gizmos.DrawLine(start, end);
            }

        for (int x = 0; x < currentGridSettings.Width; x++)
        {
            for (int y = 0; y < currentGridSettings.Height; y++)
            {
                Vector3 pos = new Vector3(
                    x * currentGridSettings.cellSize + currentGridSettings.cellSize * 0.5f,
                    0,
                    y * currentGridSettings.cellSize + currentGridSettings.cellSize * 0.5f
                ) + currentGridSettings.origin;

                Gizmos.DrawSphere(pos, 0.05f);
            }
        }

        /*   Gizmos.color = Color.black;
           for (int x = 0; x < currentGrid.Width; x++)
               for (int y = 0; y < currentGrid.Height; y++)
               {
                   Gizmos.DrawLine(new Vector3(x * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin, (new Vector3(x * currentGrid.cellSize, 0F, y + 1) * currentGrid.cellSize) + currentGrid.origin);

                   Gizmos.DrawLine(new Vector3(x * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin, (new Vector3((x + 1) * currentGrid.cellSize, 0F, y * currentGrid.cellSize) + currentGrid.origin));


               }

           Gizmos.DrawLine(new Vector3(currentGrid.Width, 0F, 0) * currentGrid.cellSize + currentGrid.origin, new Vector3(currentGrid.Width, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin);
           Gizmos.DrawLine(new Vector3(0f, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin, new Vector3(currentGrid.Width, 0F, currentGrid.Height) * currentGrid.cellSize + currentGrid.origin);

         */


    }
    #endregion
}
