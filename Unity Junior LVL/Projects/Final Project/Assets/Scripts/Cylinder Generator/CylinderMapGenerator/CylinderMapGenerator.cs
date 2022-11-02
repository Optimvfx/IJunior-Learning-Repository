using UnityEngine;

public abstract class CylinderMapGenerator <Arguments> : MonoBehaviour
    where Arguments : ICylinderMapGeneratorArguments
{
    public abstract ReadOnlyCylinderMap GetMapByArguments(Arguments arguments);
}

public interface ICylinderMapGeneratorArguments
{ }
