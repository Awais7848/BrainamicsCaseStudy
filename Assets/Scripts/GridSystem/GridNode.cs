public class GridNode<T>
{
    public GridPosition Position { get; private set; }
    private T item;

    public GridNode(GridPosition pos)
    {
        Position = pos;
    }

    public T GetItem() => item;


    public bool IsEmpty => item==null;

    public void SetItem(T value) => item = value;
    public void Clear() => item = default;
}