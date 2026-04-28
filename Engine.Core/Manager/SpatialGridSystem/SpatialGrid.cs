namespace Engine.Core.Manager.SpatialGridSystem;

/// <summary>
/// Represents a spatial grid for tracking entities and their locations within discrete cells.
/// </summary>
/// <remarks>The SpatialGrid class provides methods to add, move, and remove entities within a grid structure, as
/// well as to query the entities present in a specific cell or determine the cell occupied by a given entity. This
/// class is useful for spatial partitioning scenarios, such as collision detection or spatial queries in games and
/// simulations.</remarks>
public class SpatialGrid
{
    // Cell -> Entity
    private readonly Dictionary<Cell, List<int>?> _cells = new();
    
    // Entity -> Cell
    private readonly Dictionary<int, Cell> _entityCell = new();
    
    public static readonly (int x, int y)[] NeighborOffsets =
    [
        (1, 0),
        (0, 1),
        (1, 1),
        (-1, 1)
    ];

    public void SetEntity(int entityId, Cell newCell)
    {
        if (TryGetCell(entityId, out var oldCell))
        {
            if (oldCell.Equals(newCell))
                return;

            UpdateOldCell(entityId, oldCell);
        }

        AddEntity(entityId, newCell);
    }

    public void RemoveEntity(int entityId)
    {
        if (!TryGetCell(entityId, out var cell))
            return;

        if (_cells.TryGetValue(cell, out var list))
        {
            list?.Remove(entityId);

            if (list is { Count: 0 })
                _cells.Remove(cell);
        }

        _entityCell.Remove(entityId);
    }

    public Dictionary<Cell, List<int>?> GetCells()
    {
        return _cells;
    }
    
    public bool TryGetCell(int entityId, out Cell cell)
    {
        return _entityCell.TryGetValue(entityId, out cell);
    }

    public bool TryGetEntities(Cell cell, out List<int>? entities)
    {
        return _cells.TryGetValue(cell, out entities);
    }

    #region private methods

    private void UpdateOldCell(int entityId, Cell oldCell)
    {
        var oldList = _cells[oldCell];
        oldList?.Remove(entityId);

        if (oldList is { Count: 0 })
            _cells.Remove(oldCell);
    }

    private void AddEntity(int entityId, Cell cell)
    {
        _entityCell[entityId] = cell;

        if (!_cells.TryGetValue(cell, out var list))
        {
            list = [];
            _cells[cell] = list;
        }

        list?.Add(entityId);
    }

    #endregion
}