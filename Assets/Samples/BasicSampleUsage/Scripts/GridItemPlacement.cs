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

    [SerializeField]List<GridPosition> positionList;
    
    [SerializeField] LayerMask IgnoreMask;


    ColorBlock selectedColorBlock;
    GridPosition selected;
    GridPosition newMovePosition;

    private void Start()
    {

        sampleGrid = new GridSystem<ColorBlock>(gridWidth,gridHeight,cellSize,origin);
        gridQuery = new GridQuery<ColorBlock>(sampleGrid);
        for(int i = 0; i < positionList.Count; i++)
        {

            if (sampleGrid.IsValidPosition(positionList[i]) && sampleGrid.IsEmpty(positionList[i]))
            {

                ColorBlock obj = Instantiate(colorBlockPrefab, sampleGrid.GridToWorld(positionList[i]), Quaternion.identity);
                obj.name = "Cube" + positionList[i].ToString();
                sampleGrid.SetItem(positionList[i], obj);

            }
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {
             
                selected = sampleGrid.WorldToGrid(hit.point, true);
               // if (!sampleGrid.IsEmpty(selected))


                if (!sampleGrid.IsEmpty(selected)){
                    selectedColorBlock = sampleGrid.GetItem(selected);
                    selectedColorBlock.Highlight();
                }
                    
             }
        }


        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {
                GridPosition nextGridPosition= sampleGrid.WorldToGrid(hit.point, true);

                if (sampleGrid.IsValidPosition(nextGridPosition))
                    newMovePosition = nextGridPosition;

                if (selectedColorBlock!=null&&sampleGrid.IsValidPosition(newMovePosition)&&sampleGrid.IsEmpty(newMovePosition))
                sampleGrid.GetItem(selected).transform.position = sampleGrid.GridToWorld(newMovePosition);

            }


         
        
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (selectedColorBlock != null)
            {
                Debug.Log("Selected Position  :" + selected.ToString());
                sampleGrid.GetItem(selected).Normal();

                if (sampleGrid.IsValidPosition(newMovePosition))
                {
                    sampleGrid.SetItem(newMovePosition, selectedColorBlock);
                    sampleGrid.RemoveItem(selected);
                    Debug.Log("Move Position  :" + newMovePosition.ToString());
                    selectedColorBlock = null;
                }
            }
        }

        /*

        if (Input.GetKeyDown(KeyCode.B))
        {
            GridPosition gridPosition = new GridPosition(0, 1);
            List<GridPosition> neighbours = gridQuery.GetNeighbors(gridPosition, GridDirections.Diaognal);

            for (int i = 0; i < neighbours.Count; i++)
            {
                ColorBlock tempColorBlock = sampleGrid.GetItem(neighbours[i]);
                sampleGrid.RemoveItem(neighbours[i]);

                Destroy(tempColorBlock.gameObject);
                Debug.Log("Color Block Removed !" + tempColorBlock.name);
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

        */

    }
}
