using IJunior.TypedScenes;
using UnityEngine;

[RequireComponent(typeof(CylinderMapPlacer))]
[RequireComponent(typeof(CylinderMapGenerator<CylinderMapGeneratorImageArguments>))]
public class CylinderFieldGeneratorByImage : CylinderFieldGenerator<CylinderMapGeneratorImageArguments>
{
    private CylinderMapPlacer _cylinderMapPlacer;
    private CylinderMapGenerator<CylinderMapGeneratorImageArguments> _cylinderMapGenerator;

    public void TryGenerate(Texture2D gameMap)
    {
        if (gameMap == null || gameMap.isReadable == false)
            throw new System.MissingFieldException();

        if (_cylinderMapPlacer == null)
            _cylinderMapPlacer = GetComponent<CylinderMapPlacer>();

        if (_cylinderMapGenerator == null)
            _cylinderMapGenerator = GetComponent<CylinderMapGenerator<CylinderMapGeneratorImageArguments>>();

        TryGenerate(_cylinderMapPlacer, _cylinderMapGenerator, new CylinderMapGeneratorImageArguments(gameMap));
    }
}
