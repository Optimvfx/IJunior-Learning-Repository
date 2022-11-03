using UnityEngine;

public class WinPastAllCollected : WinCondition
{
    [SerializeField] private CylinderSpawnCounter _cylinderSpawnCounter;

    private void OnEnable()
    {
        _cylinderSpawnCounter.OnAllCollected += OnAllCollected;
    }

    private void OnDisable()
    {
        _cylinderSpawnCounter.OnAllCollected += OnAllCollected;
    }

    private void OnAllCollected()
    {
        Win();
    }
}
