using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GridInteraction : MonoBehaviour
{
    [SerializeField]int gridWidth, gridHeight;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 origin;
    [SerializeField] ColorBlock colorBlockPrefab;
    [SerializeField] GameObject pointer;


    [SerializeField]List<GridPosition> positionList;
    
    [SerializeField] LayerMask IgnoreMask;


    ColorBlock selectedColorBlock;
    GridPosition selected;
    GridPosition newMovePosition;

    private void Start()
    {

    /*    for(int i = 0; i < positionList.Count; i++)
        {

            if (sampleGrid.IsValidPosition(positionList[i]) && sampleGrid.IsEmpty(positionList[i]))
            {
              
                ColorBlock obj = Instantiate(colorBlockPrefab, sampleGrid.GridToWorld(positionList[i]), Quaternion.identity);
                for (int j = 0; j < obj.shape.Length; j++)
                {
                    GridPosition validationPosition = positionList[i] + obj.shape[j];
                    Debug.Log("Addition : "+positionList[i] +" + "+ obj.shape[j]+" = "+validationPosition.ToString());
                    if (sampleGrid.IsValidPosition(validationPosition))
                    {
                    }
                    else
                    {

                        Debug.Log("<color=red>Position is Not Valid Cant Place Shape</color>");
                        obj.gameObject.SetActive(false);
                    }
                }
                obj.name = "Cube" + positionList[i].ToString();
              

            }
        }
    */
    }



    private void Update()
    {
        SpawnGridElements();
        SelectGridElement();
        HighlightSelectedNeighbours();

    



        /* Grid Movement
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {
             
                selected = DemoGridManager.Grid.WorldToGrid(hit.point, true);

                Debug.Log("Selected Position  :" + selected.ToString());

                if (!DemoGridManager.Grid.IsEmpty(selected)){
                    selectedColorBlock = DemoGridManager.Grid.GetItem(selected);
                    selectedColorBlock.Highlight();
                }
                    
             }
        }
        */

        /* Sample Grid Operations
       
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

                */


        /*

               

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




    void SelectGridElement()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {

                selected = DemoGridManager.Grid.WorldToGrid(hit.point, true);


                if (!DemoGridManager.Grid.IsEmpty(selected))
                {

                    selectedColorBlock = DemoGridManager.Grid.GetItem(selected);

                    selectedColorBlock.Select();

                }

            }
        }




    }


    void SpawnGridElements()
    {


        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {

                selected = DemoGridManager.Grid.WorldToGrid(hit.point, true);


                if (DemoGridManager.Grid.IsEmpty(selected))
                {


                    ColorBlock colorBlock = Instantiate(colorBlockPrefab, DemoGridManager.Grid.GridToWorld(selected), Quaternion.identity);
                    DemoGridManager.Grid.SetItem(selected, colorBlock);

                }

            }
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, IgnoreMask))
            {

                selected = DemoGridManager.Grid.WorldToGrid(hit.point, true);

                if (DemoGridManager.Grid.IsValidPosition(selected) && !DemoGridManager.Grid.IsEmpty(selected))
                {
                    GridDebug.Assert("Item Removed At : " + selected.ToString());
                    selectedColorBlock = DemoGridManager.Grid.GetItem(selected);
                    DemoGridManager.Grid.RemoveItem(selected);
                    Destroy(selectedColorBlock.gameObject);

                }

            }

        }

    }


    void HighlightSelectedNeighbours()
    {

        HighLightNeighbours(KeyCode.D, GridDirections.Diaognal);
        HighLightNeighbours(KeyCode.O, GridDirections.Orthogonal);
        HighlightRow(KeyCode.R);
        HighlightColumn(KeyCode.C);

    }

    void HighlightRow(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode) && selectedColorBlock != null)
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetRow(selected);

            HighLightElements(neighbours, true);

        }

        if (Input.GetKeyUp(keyCode))
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetRow(selected);

            HighLightElements(neighbours, false);
        }
    }

    void HighlightColumn(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode) && selectedColorBlock != null)
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetColumn(selected);

            HighLightElements(neighbours, true);

        }

        if (Input.GetKeyUp(keyCode))
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetColumn(selected);

            HighLightElements(neighbours, false);
        }

    }


    void HighLightNeighbours(KeyCode keyCode, GridPosition[] direction)
    {
        if (Input.GetKeyDown(keyCode) && selectedColorBlock != null)
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetNeighbors(selected, direction);

            HighLightElements(neighbours, true);

        }

        if (Input.GetKeyUp(keyCode))
        {
            List<GridPosition> neighbours = DemoGridManager.GridQuery.GetNeighbors(selected, direction);

            HighLightElements(neighbours,false);
        }
    }


   void HighLightElements(List<GridPosition> elements,bool highLight)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if(highLight)
            DemoGridManager.Grid.GetItem(elements[i]).Highlight();
            else

                DemoGridManager.Grid.GetItem(elements[i]).Normal();

        }
    }


}
