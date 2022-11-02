using UnityEngine;

public class WinPastAllCollected : WinCondition
{
    [SerializeField] private CylinderSpawnCounter _cylinderSpawnCounter;

    private void OnEnable()
    {
        _cylinderSpawnCounter.OnAllCollected += Win;
    }

    private void OnDisable()
    {
        _cylinderSpawnCounter.OnAllCollected += Win;
    }
}
