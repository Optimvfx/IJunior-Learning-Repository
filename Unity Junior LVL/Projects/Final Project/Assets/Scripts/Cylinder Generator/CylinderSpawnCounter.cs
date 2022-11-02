using UnityEngine;
using UnityEngine.Events;

public class CylinderSpawnCounter : MonoBehaviour
{
    [SerializeField] private CylinderFieldGenerator _fieldGenerator;

    private int _activatedCount;
    private int _cylinderCount;

    public int ActivatedCount => _activatedCount;
    public int CylinderCount => _cylinderCount;

    public event UnityAction<int, int> OnCylinderActivationCountChange;

    public event UnityAction OnAllCollected;

    private void Awake()
    {
        _activatedCount = 0;
        _cylinderCount = 0;

        foreach (var placedCylinder in _fieldGenerator.Placed)
            AddCylinder(placedCylinder);
    }

    private void OnEnable()
    {
        _fieldGenerator.OnPlace += AddCylinder;
    }

    private void OnDisable()
    {
        _fieldGenerator.OnPlace -= AddCylinder;
    }

    private void AddCylinder(Cylinder cylinder)
    {
        cylinder.OnActivate += AddActivated;
        _cylinderCount++;

        OnCylinderActivationCountChange?.Invoke(_activatedCount, _cylinderCount);
    }

    private void AddActivated(Cylinder cylinder)
    {
        cylinder.OnActivate -= AddActivated;
        _activatedCount++;

        if (_activatedCount > _cylinderCount)
            throw new System.ArgumentException("Activated must by smaller then all!");

        if (_activatedCount == _cylinderCount)
            OnAllCollected?.Invoke();

        OnCylinderActivationCountChange?.Invoke(_activatedCount, _cylinderCount);
    }
}
