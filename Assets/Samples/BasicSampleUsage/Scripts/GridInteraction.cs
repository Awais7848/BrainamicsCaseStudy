using System.Collections.Generic;
using UnityEngine;

public class GridInteraction : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] int gridWidth, gridHeight;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 origin;

    [Header("References")]
    [SerializeField] ColorBlock colorBlockPrefab;
    [SerializeField] GameObject pointer;
    [SerializeField] LayerMask ignoreMask;

    private GridSystem<ColorBlock> grid;
    private ColorBlock selectedColorBlock;
    private GridPosition selected;
    private GridPosition newMovePosition;

    private void Start()
    {
        grid = DemoGridManager.Grid; 
    }

    private void Update()
    {
        HandleSpawn();
        HandleSelect();
        HandleMove();
        HandleHighlight();
    }

    #region Raycast Helpers

    private bool TryGetMouseGridPosition(out GridPosition gridPos)
    {
        gridPos = default;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, ignoreMask))
        {
            gridPos = grid.WorldToGrid(hit.point, true);
            return true;
        }
        return false;
    }

    private bool TryGetBlock(GridPosition pos, out ColorBlock block)
    {
        block = null;
        if (grid.IsValidPosition(pos) && !grid.IsEmpty(pos))
        {
            block = grid.GetItem(pos);
            return true;
        }
        return false;
    }

    #endregion

    #region Spawn Blocks

    private void HandleSpawn()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            if (TryGetMouseGridPosition(out GridPosition pos) && grid.IsEmpty(pos))
            {
                var block = Instantiate(colorBlockPrefab, grid.GridToWorld(pos), Quaternion.identity);
                grid.SetItem(pos, block);
            }
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            if (TryGetMouseGridPosition(out GridPosition pos) && TryGetBlock(pos, out ColorBlock block))
            {
                grid.RemoveItem(pos);
                Destroy(block.gameObject);
            }
        }
    }

    #endregion

    #region Select Blocks

    private void HandleSelect()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            if (TryGetMouseGridPosition(out GridPosition pos) && TryGetBlock(pos, out ColorBlock block))
            {
                selected = pos;
                selectedColorBlock = block;
                block.Select();
            }
        }
    }

    #endregion

    #region Move Blocks

    private void HandleMove()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt))
        {
            if (TryGetMouseGridPosition(out GridPosition pos) && TryGetBlock(pos, out ColorBlock block))
            {
                selected = pos;
                selectedColorBlock = block;
                block.Highlight();
                newMovePosition = pos; 
            }
        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt) && selectedColorBlock != null)
        {
            if (TryGetMouseGridPosition(out GridPosition pos) && grid.IsValidPosition(pos) && grid.IsEmpty(pos))
            {
                newMovePosition = pos;
                selectedColorBlock.transform.position = grid.GridToWorld(newMovePosition);
            }
        }

        if (Input.GetMouseButtonUp(0) && Input.GetKeyUp(KeyCode.LeftAlt)&&selectedColorBlock != null)
        {
            selectedColorBlock.Normal();
            if (grid.IsValidPosition(newMovePosition))
            {
                grid.SetItem(newMovePosition, selectedColorBlock);
                grid.RemoveItem(selected);
            }
            selectedColorBlock = null;
        }
    }

    #endregion

    #region Highlight Neighbours

    private void HandleHighlight()
    {
        if (selectedColorBlock == null) return;

        ToggleNeighbours(KeyCode.O, GridDirections.Orthogonal);

        ToggleNeighbours(KeyCode.D, GridDirections.Diaognal);

        ToggleLine(KeyCode.R, DemoGridManager.GridQuery.GetRow);

        ToggleLine(KeyCode.C, DemoGridManager.GridQuery.GetColumn);
    }

    private void ToggleNeighbours(KeyCode key, GridPosition[] directions)
    {
        if (Input.GetKeyDown(key))
        {
            HighlightElements(DemoGridManager.GridQuery.GetNeighbors(selected, directions), true);
        }
        if (Input.GetKeyUp(key))
        {
            HighlightElements(DemoGridManager.GridQuery.GetNeighbors(selected, directions), false);
        }
    }

    private void ToggleLine(KeyCode key, System.Func<GridPosition, List<GridPosition>> lineFunc)
    {
        if (Input.GetKeyDown(key))
        {
            HighlightElements(lineFunc(selected), true);
        }
        if (Input.GetKeyUp(key))
        {
            HighlightElements(lineFunc(selected), false);
        }
    }

    private void HighlightElements(List<GridPosition> positions, bool highlight)
    {
        foreach (var pos in positions)
        {
            if (TryGetBlock(pos, out ColorBlock block))
            {
                if (highlight) block.Highlight();
                else block.Normal();
            }
        }
    }

    #endregion
}