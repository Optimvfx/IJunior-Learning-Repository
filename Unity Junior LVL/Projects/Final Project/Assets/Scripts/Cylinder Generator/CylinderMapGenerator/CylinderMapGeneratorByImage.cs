using System;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMapGeneratorByImage : CylinderMapGenerator<CylinderMapGeneratorImageArguments>
{
    private readonly Color _noAlphaColor = new Color(1, 1, 1, 0);

    [SerializeField] private Color _cylinderColor;
    [SerializeField] private Color _nonColor;

    public override ReadOnlyCylinderMap GetMapByArguments(CylinderMapGeneratorImageArguments arguments)
    {
        var imageArgument = arguments.Image;

        var objectMap = new Array2D<CylinderMap.MapObjectType>(imageArgument.width, imageArgument.height);

        var colorToMapObjectConvertor = GenerateColorToMapObjectConvertor();

        for (int x = 0; x < imageArgument.width; x++)
        {
            for (int y = 0; y < imageArgument.height; y++)
            {
                objectMap[x, y] = SellectTypeByColor(imageArgument.GetPixel(x, y), colorToMapObjectConvertor);
            }
        }

        return new CylinderMap(objectMap);
    }

    private CylinderMap.MapObjectType SellectTypeByColor(Color color, Dictionary<Color, CylinderMap.MapObjectType> colorToMapObjectConvertor)
    {
        if (colorToMapObjectConvertor.ContainsKey(color * _noAlphaColor))
            return colorToMapObjectConvertor[color * _noAlphaColor];

        return CylinderMap.MapObjectType.Non;
    }

    private Dictionary<Color, CylinderMap.MapObjectType> GenerateColorToMapObjectConvertor()
    {
        var colorToMapObjectConvertor = new Dictionary<Color, CylinderMap.MapObjectType>();

        colorToMapObjectConvertor[_cylinderColor * _noAlphaColor] = CylinderMap.MapObjectType.Cylinder;
        colorToMapObjectConvertor[_nonColor * _noAlphaColor] = CylinderMap.MapObjectType.Non;

        return colorToMapObjectConvertor;
    }
}

public class CylinderMapGeneratorImageArguments : ICylinderMapGeneratorArguments
{
    public Texture2D Image { get; private set; }

    public CylinderMapGeneratorImageArguments(Texture2D image)
    {
        Image = image;
    }
}
