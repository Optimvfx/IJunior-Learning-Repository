using System;

public class ReadOnlyCylinderMap
{
    protected readonly Array2D<MapObjectType> Map;

    public int Widht => Map.Widht;
    public int Height => Map.Height;

    public ReadOnlyCylinderMap(Array2D<MapObjectType> map)
    {
        if (map == null)
            throw new System.ArgumentNullException("No Cylinder sened.");

        Map = map.Clone();
    }

    public MapObjectType this[int x, int y]
    {
        get
        {
            if (Map.OutOfBounds(x, y))
                throw new ArgumentException("Index out of range!");

            return Map[x, y];
        }
    }


    public enum MapObjectType
    {
        Cylinder,
        Non
    }
}

public class CylinderMap : ReadOnlyCylinderMap
{
    public CylinderMap(Array2D<MapObjectType> map) : base(map)
    { }

    public new MapObjectType this[int x, int y]
    {
        set
        {
            if (Map.OutOfBounds(x, y))
                throw new ArgumentException("Index out of range!");

            Map[x, y] = value;
        }
    }
}