using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GridItemPlacement : MonoBehaviour
{
    [SerializeField]int gridWidth, gridHeight;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 origin;
    [SerializeField] ColorBlock colorBlockPrefab;
    [SerializeField] GameObject pointer;
    GridSystem<ColorBlock> sampleGrid;
    GridQuery<ColorBlock> gridQuery;
    private void Start()
    {

        sampleGrid = new GridSystem<ColorBlock>(gridWidth,gridHeight,cellSize,origin);
        gridQuery = new GridQuery<ColorBlock>(sampleGrid);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                pointer.transform.position = hit.point;
                GridPosition gridPosition = sampleGrid.WorldToGrid(hit.point,true);
                Debug.Log(gridPosition.ToString());

                if (sampleGrid.IsValidPosition(gridPosition)&&sampleGrid.IsEmpty(gridPosition)){

                    ColorBlock obj = Instantiate(colorBlockPrefab, sampleGrid.GridToWorld(gridPosition), Quaternion.identity);
                    obj.name = "Cube" + gridPosition.ToString();
                    sampleGrid.SetItem(gridPosition,obj);
                    
                }
                Debug.Log("Here !");
            }
        }
        if (Input.GetKeyDown(KeyCode.B) ){
            GridPosition gridPosition = new GridPosition(0, 1);
           List<GridPosition> neighbours= gridQuery.GetNeighbors(gridPosition,GridDirections.Diaognal);

            for (int i = 0; i < neighbours.Count; i++)
            {
                ColorBlock tempColorBlock = sampleGrid.GetItem(neighbours[i]);
                sampleGrid.RemoveItem(neighbours[i]);

                Destroy(tempColorBlock.gameObject);
                Debug.Log("Color Block Removed !"+ tempColorBlock.name);
            }
        
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GridPosition gridPosition = new GridPosition(1, 1);
            List<GridPosition> neighbours = gridQuery.GetRow(gridPosition);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Debug.Log("Color Block Removed !" + neighbours[i].ToString());
                ColorBlock tempColorBlock = sampleGrid.GetItem(neighbours[i]);
                sampleGrid.RemoveItem(neighbours[i]);

                Destroy(tempColorBlock.gameObject);
            }

        }

    }
}
